#include <vector>
#include <xmemory>

#include "..\Utils\num2str.hpp"
#include "..\Core\Types.hpp"

namespace matcpp
{
	template<typename dtype, typename Allocator = std::allocator<dtype>>
	class Mat
	{
	public:
		Mat() = default;

	private:
		using value_type = dtype;
		using allocator_type = Allocator;
		using size_type = uint32;
		using reference = dtype&;
		using const_reference = const dtype&;

	};
}