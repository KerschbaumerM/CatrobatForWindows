﻿using Catrobat.IDE.Core.Formulas;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaTree
{
    partial interface IFormulaTree : IFormulaTokenizer, IStringBuilderSerializable, IFormulaInterpreter, IFormulaEvaluation, IXmlFormulaConvertible
    {
    }
}
