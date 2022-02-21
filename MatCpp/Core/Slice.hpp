#pragma one

#include <string>
#include "Types.hpp"
#include "..\Utils\num2str.hpp"

namespace matcpp
{
	class Slice
	{
	public:
		int32 start{ 0 };
		int32 stop{ 1 };
		int32 step{ 1 };

		constexpr explicit Slice(int32 inStop) noexcept:
			stop(inStop)
		{}

		constexpr Slice(int32 inStart,int32 inStop) noexcept:
			start(inStart),
			stop(inStop)
		{}

		constexpr Slice(int32 inStart,int32 inStop,int32 inStep) noexcept:
			start(inStart),
			stop(inStop),
			step(inStep)
		{}

		bool operator==(const Slice& inOtherSlice) const noexcept
		{
			return start == inOtherSlice.start && stop == inOtherSlice.stop && step == inOtherSlice.step;
		}

		bool operator!=(const Slice& inOtherSlice) const noexcept
		{
			return !(*this == inOtherSlice);
		}

		std::string str() const
		{
			std::string out = "[" + utils::num2str(start) + ":" + 
				utils::num2str(stop) + ":" + utils::num2str(step) + "]\n";
			return out;
		}




	};
}