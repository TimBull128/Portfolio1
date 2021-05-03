#include "stringFunctions.h"
class stringFunctions
{
public:
	char* ToChar(string input = "") {
		char arr[] = "";

		strcpy(arr, input.c_str());
		return arr;
	}
	string ToString(char input[]) {
		string input = "";
		for (int i = 0; i < sizeof(input); i++)
		{
			input += input[i];
		}
		return input;
	}

};