#pragma once
#include "User.h"
class Staff : public User
{
	public:
		TestTable newEntry();
		void addEntry(TestTable input);
		void editEntry(TestTable input);
		void delEntry(int ID);
};

