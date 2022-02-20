#pragma one

#include <string>

namespace matcpp
{
	namespace utils
	{
		template<class dtype>
		std::string num2str(dtype num)
		{
			return std::to_string(num);
		}
	}
}