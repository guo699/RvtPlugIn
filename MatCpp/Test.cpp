#include <iostream>
#include <string>
#include <vector>
#include <cstdint>
#include <algorithm>
#include <typeinfo>

using namespace std;


int main(int argc, char* args[])
{
	int n;

	malloc(1024 * 1024 * 1024);
	cin >> n;

	cout << n + 1 << endl;


	system("pause");
}
