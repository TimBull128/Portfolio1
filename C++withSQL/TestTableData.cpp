#include <sql.h>;
#include <sqlext.h>;
#include <sqltypes.h>;
#include <Windows.h>;
#include <vector>;
#include <string>;
using namespace std;
class testDataLayer {
public:
	vector<TestTable> getAll() {
		vector<TestTable> result;
		int ID = 0;


		char SQLQuery[] = "SELECT * from TestTable";
		this->queryDatabase(SQLQuery);

		while (SQLFetch(SQLStatementHandle) == SQL_SUCCESS)
		{
			SQLGetData(SQLStatementHandle,0,&database.ID)
			result.push_back(database);
		}
		this->clearQuery();
		return result;
	};
private:
	SQLHANDLE SQLStatementHandle;
	SQLHANDLE SQLEnvHandle = NULL;
	SQLHANDLE SQLConnectionHandle = NULL;

	void clearQuery() {
		SQLFreeHandle(SQL_HANDLE_STMT, this->SQLStatementHandle);
		SQLDisconnect(this->SQLConnectionHandle);
		SQLFreeHandle(SQL_HANDLE_DBC, this->SQLConnectionHandle);
		SQLFreeHandle(SQL_HANDLE_ENV, this->SQLEnvHandle);
	}
	void queryDatabase(char SQLQuery[]) {
		this->SQLEnvHandle = NULL;
		this->SQLConnectionHandle = NULL;
		this->SQLStatementHandle = NULL;
		SQLRETURN retCode = 0;
		SQLCHAR retConString[1024] = "";

		do {
			//Allocate the Handle for the environment
			if (SQL_SUCCESS != SQLAllocHandle(SQL_HANDLE_ENV, SQL_NULL_HANDLE, &SQLEnvHandle))
			{
				break;
			}
			//Set the version
			if (SQL_SUCCESS != SQLSetEnvAttr(SQLEnvHandle, SQL_ATTR_ODBC_VERSION, (SQLPOINTER)SQL_OV_ODBC3, 0))
			{
				break;
			}
			//Set up the ConnectionHandle
			if (SQL_SUCCESS != SQLAllocHandle(SQL_HANDLE_DBC, SQLEnvHandle, &SQLConnectionHandle))
			{
				break;
			}
			if (SQL_SUCCESS != SQLSetConnectAttr(SQLConnectionHandle, SQL_LOGIN_TIMEOUT, (SQLPOINTER)5, 0))
			{
				break;
			}
			//Connect to the database
			switch (SQLDriverConnect(SQLConnectionHandle, NULL,
				(SQLCHAR*)
				/*Alter for database settings
					DRIVER = Type of Database
					SERVER = IP Address
					DATABASE = Database Name
					UID = ID of login
					PWD = Password of login
				*/
				"Driver = {SQL Server}; SERVER=localhost, 1433; DATABASE = ExampleDB; UID = ExampleDBOwner; PWD = ExamplePW ",
				SQL_NTS, retConString, 1024, NULL, SQL_DRIVER_NOPROMPT))
			{

			case SQL_SUCCESS:
				break;
			case SQL_SUCCESS_WITH_INFO:
				break;
			case SQL_NO_DATA_FOUND:
				this->showSQLError(SQL_HANDLE_DBC, SQLConnectionHandle);
				retCode = -1;
				break;
			case SQL_INVALID_HANDLE:
				this->showSQLError(SQL_HANDLE_DBC, SQLConnectionHandle);
				break;
			default:
				break;
			}
			if (retCode == -1) {
				break;
			}
			if (SQL_SUCCESS != SQLAllocHandle(SQL_HANDLE_STMT, SQLConnectionHandle, &SQLStatementHandle)) {
				break;
			}
			if (SQL_SUCCESS != SQLExecDirect(SQLStatementHandle, (SQLCHAR*)SQLQuery, SQL_NTS)) {
				this->showSQLError(SQL_HANDLE_STMT, SQLStatementHandle);
				break;
			}
			else {
				break;
			}
		} while (false);
	};
	void showSQLError(unsigned int handleType, const SQLHANDLE& handle)
	{
		SQLCHAR SQLState[1024];
		SQLCHAR message[1024];
		if (SQL_SUCCESS == SQLGetDiagRec(handleType, handle, 1, SQLState, NULL, message, 1024, NULL)) {

			cout << "SQL Driver message: " << message << "\nSQL State: " << SQLState << "." << endl;
		}
	}

};
class TestTable {
public:
	int ID = 0;
	char firstName;
	char lastName;
};