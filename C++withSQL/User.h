#pragma once
#include "TestTableDataLayer.h"
#include "stringFunctions.h"
#include <vector>
using namespace std;
class User
{
	public:
		vector<TestTable> GetAll();
		TestTable GetEntry(int ID);
	private:
		TestTable Database;
		TestTableDataLayer DataRepository;

};

