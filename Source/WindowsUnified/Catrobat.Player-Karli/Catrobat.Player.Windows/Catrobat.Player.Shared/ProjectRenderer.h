#pragma once

#include "Direct3DBase.h"
#include "SpriteBatch.h"
#include "PrimitiveBatch.h"
#include "SpriteFont.h"
#include "Project.h"
#include "ProjectRenderer.h"
#include "VertexTypes.h" 

ref class ProjectRenderer sealed : public Direct3DBase
{
public:
	ProjectRenderer();
    virtual ~ProjectRenderer();

	// Direct3DBase methods.
	virtual void CreateDeviceResources() override;
	virtual void CreateWindowSizeDependentResources() override;
	virtual void Render() override;

	// Method for updating time-dependent objects.
	void Update(float timeTotal, float timeDelta);

	void StartUpTasks();

private:
	std::unique_ptr<SpriteBatch> m_spriteBatch;
	std::unique_ptr<SpriteFont> m_spriteFont; 
	bool m_Initialized;
};

