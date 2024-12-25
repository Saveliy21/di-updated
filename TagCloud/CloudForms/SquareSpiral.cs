using System.Drawing;

namespace TagCloud.CloudForms;

public class SquareSpiral(Point centre) : ICloudForm
{
    private const int Step = 1;
    private int _stepCounter = 1;
    private int _neededSteps = 1;
    private Direction _direction = Direction.Up;
    private Point _previus = centre;

    public Point GetNextPoint()
    {
        _previus += GetOffsetSize(_direction);

        _stepCounter--;

        if (_stepCounter == 0)
        {
            _direction = _direction.ClockwiseRotate();

            if (_direction == Direction.Up || _direction == Direction.Down)
            {
                _neededSteps++;
            }

            _stepCounter = _neededSteps;
        }

        return _previus;
    }

    private Size GetOffsetSize(Direction direction) => direction switch
    {
        Direction.Up => new Size(0, Step),
        Direction.Right => new Size(Step, 0),
        Direction.Down => new Size(0, -Step),
        Direction.Left => new Size(-Step, 0),
        _ => throw new ArgumentOutOfRangeException(nameof(direction))
    };
}