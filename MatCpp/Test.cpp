#include <iostream>
#include <string>
#include <vector>
#include <cstdint>
#include <algorithm>
#include <typeinfo>

using namespace std;

class Dog
{
public:
	int age;
};

void setAge(Dog& dog)
{
	dog.age += 200;
}

int main(int argc, char* args[])
{
	Dog d;
	d.age = 30;

	setAge(d);

	cout << d.age << endl;

	system("pause");
}
