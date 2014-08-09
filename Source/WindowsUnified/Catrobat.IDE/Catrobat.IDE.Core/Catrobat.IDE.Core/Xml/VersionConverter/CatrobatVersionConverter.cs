﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Utilities;
using Catrobat.IDE.Core.Xml.VersionConverter.Versions;

namespace Catrobat.IDE.Core.Xml.VersionConverter
{
    public static class CatrobatVersionConverter
    {
        public enum VersionConverterStatus
        {
            NoError, VersionTooNew, VersionTooOld, ProgramDamaged, ProgramPathNotValid
        }

        private static Dictionary<CatrobatVersionPair, CatrobatVersion> Converters
        {
            get
            {
                return new Dictionary<CatrobatVersionPair, CatrobatVersion>(new CatrobatVersionPair.EqualityComparer())
                {
                    {new CatrobatVersionPair("0.91", "Win0.91"), new CatrobatVersion091ToWin091()},
                    {new CatrobatVersionPair("0.92", "0.91"), new CatrobatVersion092To091()}
                };
            }
        }

        private static Dictionary<CatrobatVersionPair, List<CatrobatVersionPair>> ConvertersPaths
        {
            get
            {
                return new Dictionary<CatrobatVersionPair, List<CatrobatVersionPair>>(new CatrobatVersionPair.EqualityComparer())
                {
                    {
                        new CatrobatVersionPair("0.91", "Win0.91"),

                        new List<CatrobatVersionPair>
                        {
                            new CatrobatVersionPair("0.91", "Win0.91", false)
                        }
                    },
                    {
                       new CatrobatVersionPair("Win0.91", "0.91"),

                        new List<CatrobatVersionPair>
                        {
                            new CatrobatVersionPair("Win0.91", "0.91", true)
                        }
                    },


                    {
                        new CatrobatVersionPair("0.92", "Win0.91"),

                        new List<CatrobatVersionPair>
                        {
                            new CatrobatVersionPair("0.92", "0.91", false),
                            new CatrobatVersionPair("0.91", "Win0.91", false),
                        }
                    },
                    {
                        new CatrobatVersionPair("Win0.91", "0.92"),

                        new List<CatrobatVersionPair>
                        {
                            new CatrobatVersionPair("0.91", "Win0.91", true),
                            new CatrobatVersionPair("0.92", "0.91", true),
                        }
                    },


                    {
                        new CatrobatVersionPair("0.92", "0.91"),

                        new List<CatrobatVersionPair>
                        {
                            new CatrobatVersionPair("0.92", "0.91", false)
                        }
                    },
                    {
                       new CatrobatVersionPair("0.91", "0.92"),

                        new List<CatrobatVersionPair>
                        {
                            new CatrobatVersionPair("0.92", "0.91", true)
                        }
                    },
                };
            }
        }

        public static VersionConverterStatus ConvertVersions(string inputVersion, string outputVersion, XDocument document)
        {
            if (inputVersion == outputVersion)
            {
                return VersionConverterStatus.NoError;
            }


            var error = VersionConverterStatus.NoError;
            var versionPair = new CatrobatVersionPair(inputVersion, outputVersion);

            if (double.Parse(inputVersion, NumberStyles.Any) < Constants.MinimumCodeVersion)
            {
                return VersionConverterStatus.VersionTooOld;
            }

            if (ConvertersPaths.ContainsKey(versionPair))
            {
                var path = ConvertersPaths[versionPair];

                if (path.Count == 0)
                {
                    error = VersionConverterStatus.VersionTooOld;
                }
                else
                {
                    try
                    {
                        foreach (var pair in path)
                        {
                            if (pair.IsInverse)
                            {
                                var inversePath = new CatrobatVersionPair(pair.OutputVersion, pair.InputVersion);
                                Converters[inversePath].ConvertBack(document);
                            }
                            else
                            {
                                Converters[pair].Convert(document);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        error = VersionConverterStatus.ProgramDamaged;
                    }
                }
            }
            else
            {
                error = VersionConverterStatus.ProgramDamaged;
            }

            return error;
        }

        private static string GetInputVersion(XDocument document)
        {
            var inputVersion = "";
            var program = document.Element("program");

            if (program != null && program.Element("header") != null)
            {
                var languageVersion = program.Element("header").Element("catrobatLanguageVersion");

                if (languageVersion != null)
                {
                    inputVersion = languageVersion.Value;
                }
            }

            return inputVersion;
        }

        public static async Task<VersionConverterResult> ConvertToXmlVersion(string projectCodePath, string targetVersion, bool overwriteProject = false)
        {
            VersionConverterStatus error;
            var xml = "";

            if (!string.IsNullOrEmpty(projectCodePath))
            {
                string projectCode;

                using (var storage = StorageSystem.GetStorage())
                {
                    projectCode = await storage.ReadTextFileAsync(projectCodePath);
                }

                if (projectCode != null)
                {
                    var document = XDocument.Load(new StringReader(projectCode));
                    document.Declaration = new XDeclaration("1.0", "UTF-8", "yes");

                    var inputVersion = GetInputVersion(document);

                    if (inputVersion == Constants.TargetIDEVersion)
                    {
                        return new VersionConverterResult
                        {
                            Error = VersionConverterStatus.NoError,
                            Xml = projectCode
                        };
                    }

                    if (double.Parse(inputVersion) < Constants.MinimumCodeVersion)
                    {
                        return new VersionConverterResult
                        {
                            Error = VersionConverterStatus.VersionTooOld,
                            Xml = null
                        };
                    }

                    error = ConvertVersions(inputVersion, targetVersion, document);

                    if (error == VersionConverterStatus.NoError)
                    {
                        var writer = new XmlStringWriter();
                        document.Save(writer, SaveOptions.None);
                        xml = writer.GetStringBuilder().ToString();


                        if (overwriteProject)
                        {
                            using (var storage = StorageSystem.GetStorage())
                            {
                                try
                                {
                                    await storage.WriteTextFileAsync(projectCodePath, xml);
                                }
                                catch
                                {
                                    error = VersionConverterStatus.ProgramDamaged;
                                }
                            }
                        }
                    }
                    else
                    {
                        error = VersionConverterStatus.ProgramDamaged;
                    }
                }
                else
                {
                    error = VersionConverterStatus.ProgramDamaged;
                }
            }
            else
            {
                error = VersionConverterStatus.ProgramPathNotValid;
            }

            return new VersionConverterResult { Xml = xml, Error = error };
        }

        public static async Task<VersionConverterResult> ConvertToXmlVersionByProjectName(string projectName, string targetVersion, bool overwriteProject = false)
        {
            var projectCodePath = Path.Combine(StorageConstants.ProgramsPath, projectName, StorageConstants.ProgramCodePath);
            var result = await ConvertToXmlVersion(projectCodePath, targetVersion, overwriteProject);
            return result;
        }
    }
}
