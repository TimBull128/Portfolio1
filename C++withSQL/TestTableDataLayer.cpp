#include "TestTableDataLayer.h"
class TestTableDataLayer {
public:
    vector <TestTable> getList() {
        return this->get();
    }
    TestTable getEntry(int ID) {
        for (TestTable item : this->getList())
        {
            if (item.id == ID) { return item; break; }
        }
        cout << "Error: Entry not found";
    }
    void deleteItem(int ID) {
        this->deleteEntry(ID);
    }
    void updateItem(TestTable Input) {
        this->update(Input);
    }
    void addItem(TestTable Input) {
        this->addEntry(Input);
    }


private:
    stringFunctions str;
    #define SQL_RESULT_LEN 240
    #define SQL_RETURN_CODE_LEN 1000
    #define SQL_CONNECTION_ERROR -1
    #define SQL_QUERY_ERROR -2
    SQLHANDLE sqlConnHandle = NULL;
    SQLHANDLE sqlStmtHandle = NULL;
    SQLHANDLE sqlEnvHandle;
    SQLCHAR retconstring[SQL_RETURN_CODE_LEN];
    TestTable databaseModel;
    /*-------------------------
    ---------------------------
    ---------------------------*/
    void addEntry(TestTable input) {
        //Get the Query - was having issues adding second column - Error: expression must have integral or unscoped enum type
        string Query = "insert into TestTable (ID, firstName, secondName) values (" + input.id;
        Query.append(", " + input.firstName);
        Query.append(", " + input.secondName);
        Query.append(")");

        //Open connection
        if (this->openConnection() == -1) {
            cout << "SQL Connection error";
            goto COMPLETED;
        }

        //Run Query
        if (SQL_SUCCESS != SQLExecDirect(sqlStmtHandle, (SQLCHAR*)str.ToChar(Query), SQL_NTS)) {
            cout << "SQL Query error";
            goto COMPLETED;
        }

        //Close connection
    COMPLETED:
        this->closeConnection();
    }
    void deleteEntry(int ID) {
        //Get the Query
        string Query = "Delete from TestTable where ID == " + ID;

        //Open connection
        if (this->openConnection() == -1) {
            cout << "SQL Connection error";
            goto COMPLETED;
        }

        //Run Query
        if (SQL_SUCCESS != SQLExecDirect(sqlStmtHandle, (SQLCHAR*)str.ToChar(Query), SQL_NTS)) {
            cout << "SQL Query error";
            goto COMPLETED;
        }

        //Close connection
    COMPLETED:
        this->closeConnection();

    }
    void update(TestTable input)
    {
        string Query = "update TestTable set firstName = " + input.firstName + ", secondName = " + input.secondName + "Where CustomerID = " + to_string(input.id);
        if (this->openConnection() == -1) {
            cout << "SQL Connection error";
            goto COMPLETED;
        }

        if (SQL_SUCCESS != SQLExecDirect(sqlStmtHandle, (SQLCHAR*)str.ToChar(Query), SQL_NTS)) {
            cout << "SQL Query error";
            goto COMPLETED;
        }
    COMPLETED:
        this->closeConnection();
    }
    vector<TestTable> get()
    {
        TestTable Database;
        vector<TestTable> Result;
        string Query = "select * from TestTable";
        if (this->openConnection() == -1) {
            cout << "SQL Connection error";
            goto COMPLETED;
        }

        if (SQL_SUCCESS != SQLExecDirect(sqlStmtHandle, (SQLCHAR*)str.ToChar(Query), SQL_NTS)) {
            cout << "SQL Query error";
            goto COMPLETED;
        }
        else {
            //declare output variable and pointer

            while (SQLFetch(sqlStmtHandle) == SQL_SUCCESS) {
                SQLGetData(sqlStmtHandle, 0, SQL_C_DEFAULT, &Database.id, sizeof(Database.id), NULL);
                SQLGetData(sqlStmtHandle, 1, SQL_C_DEFAULT, &Database.firstName, sizeof(Database.firstName), NULL);
                SQLGetData(sqlStmtHandle, 2, SQL_C_DEFAULT, &Database.secondName, sizeof(Database.secondName), NULL);
                Result.push_back(Database);
            }
            return Result;
        }
    COMPLETED:
        this->closeConnection();
    }

    int openConnection() {
        //allocations
        if (SQL_SUCCESS != SQLAllocHandle(SQL_HANDLE_ENV, SQL_NULL_HANDLE, &sqlEnvHandle))
            return SQL_CONNECTION_ERROR;
        if (SQL_SUCCESS != SQLSetEnvAttr(sqlEnvHandle, SQL_ATTR_ODBC_VERSION, (SQLPOINTER)SQL_OV_ODBC3, 0))
            return SQL_CONNECTION_ERROR;
        if (SQL_SUCCESS != SQLAllocHandle(SQL_HANDLE_DBC, sqlEnvHandle, &sqlConnHandle))
            return SQL_CONNECTION_ERROR;
        //output

        //connect to SQL Server  
        //I am using a trusted connection and port 14808
        //it does not matter if you are using default or named instance
        //just make sure you define the server name and the port
        //You have the option to use a username/password instead of a trusted connection
        //but is more secure to use a trusted connection
        switch (SQLDriverConnect(sqlConnHandle,
            NULL,
            //(SQLWCHAR*)L"DRIVER={SQL Server};SERVER=localhost, 1433;DATABASE=master;UID=username;PWD=password;",
            (SQLCHAR*)L"DRIVER={SQL Server};SERVER=localhost, 1433;DATABASE=master;Trusted=true;",
            SQL_NTS,
            retconstring,
            1024,
            NULL,
            SQL_DRIVER_NOPROMPT)) {
        case SQL_SUCCESS:
            break;
        case SQL_SUCCESS_WITH_INFO:
            break;
        case SQL_INVALID_HANDLE:
            return SQL_CONNECTION_ERROR;
        case SQL_ERROR:
            return SQL_CONNECTION_ERROR;
        default:
            break;
        }
        //if there is a problem connecting then exit application
        if (SQL_SUCCESS != SQLAllocHandle(SQL_HANDLE_STMT, sqlConnHandle, &sqlStmtHandle))
            return SQL_CONNECTION_ERROR;
    }
    void closeConnection() {
        SQLFreeHandle(SQL_HANDLE_STMT, sqlStmtHandle);
        SQLDisconnect(sqlConnHandle);
        SQLFreeHandle(SQL_HANDLE_DBC, sqlConnHandle);
        SQLFreeHandle(SQL_HANDLE_ENV, sqlEnvHandle);
    }
};
