#include "User.h"

class User
{
public:
	vector<TestTable> GetAll() {
		return DataRepository.getList();
	}
	TestTable GetEntry(int ID) {
		return DataRepository.getEntry(ID);
	}
private:
	TestTable Database;
	TestTableDataLayer DataRepository;
};