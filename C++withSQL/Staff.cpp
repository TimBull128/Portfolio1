#include "Staff.h"
class Staff : public User
{
	public:
		TestTable newEntry() {
			int maxID = 0;
			for (TestTable item : DataRepository.getList()) 
			{
				maxID < item.id ? maxID = item.id : maxID = maxID;
			}
			Database.id = maxID + 1;
			Database.firstName = "";
			Database.secondName = "";
			return Database;
		}
		void addEntry(TestTable input) {
			DataRepository.addItem(input);
		}
		void editEntry(TestTable input) {
			DataRepository.updateItem(input);
		}
		void delEntry(int ID) {
			DataRepository.deleteItem(ID);
		}
	private:
		TestTable Database;
		TestTableDataLayer DataRepository;
};