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

template<class T>
class Mat
{
public:
	Mat(int row, int col)
	{
		_row = row;
		_col = col;
	}

	constexpr int size() const noexcept
	{
		return _row * _col;
	}

private:
	int _row, _col;
};

int main()
{
	Mat<double> m(11, 12);
	constexpr int ss = m.size();
	return 0;
}
