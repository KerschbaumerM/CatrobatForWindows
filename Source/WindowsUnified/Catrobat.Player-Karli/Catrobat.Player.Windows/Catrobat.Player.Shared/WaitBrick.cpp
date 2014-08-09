#include "pch.h"
#include "WaitBrick.h"
#include <windows.h>
#include <ppltasks.h>
#include "Interpreter.h"

WaitBrick::WaitBrick(FormulaTree *timeToWaitInSeconds, Script *parent) :
	Brick(TypeOfBrick::WaitBrick, parent), m_timeToWaitInSeconds(timeToWaitInSeconds)
{
}

void WaitBrick::Execute()
{
	Concurrency::wait(1000 * Interpreter::Instance()->EvaluateFormulaToInt(m_timeToWaitInSeconds, GetParent()->GetParent()));
}