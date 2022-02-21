#pragma one

#include <initializer_list>
#include <string>
#include <sstream>

namespace matcpp
{
	class Vec2
	{
	public:
		double x{ 0.0 };
		double y{ 0.0 };

		constexpr Vec2(double inX, double inY) noexcept
		{
			x = inX;
			y = inY;
		}

		Vec2(const std::initializer_list<double>& inList)
		{
			x = *inList.begin();
			y = *(inList.begin() + 1);
		}

		double distance(const Vec2& otherVec) const noexcept
		{
			return 0.0;
		}

		double dot(const Vec2& otherVec) const noexcept
		{
			return x * otherVec.x + y * otherVec.y;
		}

		static Vec2 down() noexcept
		{
			return Vec2(0.0, -1.0);
		}

		static Vec2 left() noexcept
		{
			return Vec2(-1.0, 0.0);
		}

		static Vec2 right() noexcept
		{
			return Vec2(1.0, 0.0);
		}

		static Vec2 up() noexcept
		{
			return Vec2(0.0, 1.0);
		}

		std::string tostring() const
		{
			std::stringstream stream;
			stream << "Vec2[" << x << "," << y << "]";
			return stream.str();
		}


	};
}