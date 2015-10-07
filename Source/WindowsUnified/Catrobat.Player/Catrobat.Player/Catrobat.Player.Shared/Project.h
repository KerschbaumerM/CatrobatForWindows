#pragma once

#include "Object.h"
#include "UserVariable.h"

#include <vector>

class Project
{
public:
	Project(
		std::string							applicationBuildName,
		int									applicationBuildNumber,
		std::string							applicationName,
		std::string							applicationVersion,
		std::string							catrobatLanguageVersion,
		time_t								dateTimeUpload,
		std::string							description,
		std::string							deviceName,
		std::string							mediaLicense,
		std::string							platform,
		int									platformVersion,
		std::string							programLicense,
		std::string							programName,
		std::string							remixOf,
		int									screenHeight,
		int									screenWidth,
		std::vector<std::string>			tags,
		std::string							url,
		std::string							userHandle
		);

	Project();

	~Project();

	void								Render(const std::shared_ptr<DX::DeviceResources>& deviceResources);
	void								LoadTextures(const std::shared_ptr<DX::DeviceResources>& deviceResources);
	void                                SetupWindowSizeDependentResources(const std::shared_ptr<DX::DeviceResources>& deviceResources);
	void                                CheckProjectScreenSize();
	void								StartUp();

	// Getters
	int									    GetScreenHeight();
	int									    GetScreenWidth();
	std::string								GetProgramName();
	std::string								GetCatrobatLanguageVersion() { return m_catrobatLanguageVersion; }
	std::map<std::string, std::shared_ptr<Object> >		GetObjectList();
	std::shared_ptr<UserVariable>						    GetVariable(std::string name);

	//For HeaderTest
	std::string								GetApplicationBuildName() { return m_applicationBuildName; }
	int										GetApplicationBuildNumber() { return m_applicationBuildNumber; }
	std::string								GetApplicationName() { return m_applicationName; }

	std::string								GetApplicationVersion() { return m_applicationVersion; }
	time_t									GetDateTimeUpload() { return m_dateTimeUpload; }
	std::string								GetDescription() { return m_description; }
	std::string								GetDeviceName() { return m_deviceName; }
	std::string								GetMediaLicense() { return m_mediaLicense; }
	std::string								GetPlatform() { return m_platform; }
	int										GetPlatformVersion() { return m_platformVersion; }
	std::string								GetProgramLicense() { return m_programLicense; }
	std::string								GetRemixOf() { return m_remixOf; }
	std::vector<std::string>				GetTags() { return m_tags; }
	std::string								GetUrl() { return m_url; }
	std::string								GetUserHandle() { return m_userHandle; }
	//end for HeaderTest


	// Adders
	void								AddVariable(std::string name, std::shared_ptr<UserVariable> variable);
	void								AddVariable(std::pair<std::string, std::shared_ptr<UserVariable> > variable);
	void								AddObject(std::pair<std::string, std::shared_ptr<Object> > object);

private:

	// Project Header
	std::string							m_applicationBuildName;
	int									m_applicationBuildNumber;
	std::string							m_applicationName;
	std::string							m_applicationVersion;
	std::string							m_catrobatLanguageVersion;
	time_t								m_dateTimeUpload;
	std::string							m_description;
	std::string							m_deviceName;
	std::string							m_mediaLicense;
	std::string							m_platform;
	int									m_platformVersion;
	std::string							m_programLicense;
	std::string							m_programName;
	std::string							m_remixOf;
	int									m_screenHeight;
	int									m_screenWidth;
	std::vector<std::string>			m_tags;
	std::string							m_url;
	std::string							m_userHandle;

	std::map<std::string, std::shared_ptr<Object> >	 m_objectList;
	std::map<std::string, std::shared_ptr<Object> >	 m_objectListInitial;
	std::map<std::string, std::shared_ptr<UserVariable> >   m_variableList;
	std::map<std::string, std::string>     m_variableListValueInitial;
};

