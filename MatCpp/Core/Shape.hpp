#pragma one

#include <iostream>
#include "Types.hpp"
#include "..\Utils\num2str.hpp"

namespace matcpp
{
	class Shape
	{
	public:
		uint32 rows{ 0 };
		uint32 cols{ 0 };

		constexpr explicit Shape(uint32 inSquareSize) noexcept:
			rows(inSquareSize),
			cols(inSquareSize)
		{}

		constexpr Shape(uint32 inRows,uint32 inCols) noexcept:
			rows(inRows),
			cols(inCols)
		{}

		bool operator==(const Shape& inOtherShape) const noexcept
		{
			return rows == inOtherShape.rows && cols == inOtherShape.cols;
		}

		bool operator!=(const Shape& inOtherShape) const noexcept
		{
			return rows != inOtherShape.rows || cols != inOtherShape.cols;
		}

		uint32 size() const noexcept
		{
			return rows * cols;
		}

		bool isnull() const noexcept
		{
			return rows == 0 && cols == 0;
		}

		bool issquare() const noexcept
		{
			return rows == cols;
		}

		std::string str() const
		{
			std::string out = "[" + utils::num2str(rows) + "," + utils::num2str(cols) + "]\n";
			return out;
		}

	};
}