#include <iostream>
#include <string>
#include <vector>
#include <cstdint>

using namespace std;

constexpr uint64_t sum(int start)
{
	if (start == 1)
		return 1;
	else
		return start + sum(start - 1);
}

int main()
{
	constexpr uint64_t ss = sum(500);
	cout << ss << endl;
	return 0;
}
