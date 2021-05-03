
#include <windows.h>
#include <sqlext.h>
#include <sqltypes.h>
#include <sql.h>
#include <vector>
#include <string>
#include <iostream>
#include "stringFunctions.h"
#include "TestTable.h"

using namespace std;

class TestTableDataLayer {
public:
    vector <TestTable> getList();
    TestTable getEntry(int ID);
    void deleteItem(int ID);
    void updateItem(TestTable Input);
    void addItem(TestTable Input);
    

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
    void addEntry(TestTable input);
    void deleteEntry(int ID);
    void update(TestTable input);
    vector<TestTable> get();

    int openConnection();
    void closeConnection();
};

class TestTable 
{
    public:
        int id;
        string firstName;
        string secondName;
    private:

};