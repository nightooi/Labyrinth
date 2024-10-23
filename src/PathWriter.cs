using Labyrinth.Composition.Interfaces;

using System.Text;
///////////////////////////////////////////////////////////
///                 *
///      ************************************************U
///      d          *                                    U  insert when hitting '\r\n'
///      d          *******                              U
///      d                *                              U
///      d                *    U**************************
///      d             ****    U           *************    overwrite everything else          
///      d             *       U           *           *
///      d             ********U******     *           *
///      d                     U     *     *           *
///      d                     U     *******           *
///      d                     U                       *
///      d                     *************************
///      d
///      d
///      d*********
/// 
/// 
/// 
/// 
/// 
/// 
/// 
/// 
/// 
/// 
/// 
///
namespace Labyrinth.Composition;
public class PathWriter : IPathWriter, ICloneable
{
    #region Constructors
    public PathWriter(PathWriter writer)
    {
        for (int i = 0; i < writer.LastX; i++) AdjustPadding(Padding.Add);
        this.LastY = writer._lastY;
        this.PreviousInsert = writer.PreviousInsert;
        this._chars = writer._chars;
    }
    public PathWriter(
        ISimpleFactory<GeneralPadding> padding,
        ISimpleFactory<Characters> car,
        IParameterizedFactory<ILineBank> LineBank)
    {
        _generalPadding = padding.Create();
        _chars = car.Create();
    }
    #endregion
    private ILineBank LineBank { get; init; }
    private GeneralPadding _generalPadding { get; set; }
    private StringBuilder _currentPadding = new StringBuilder();
    private Characters _chars;
    private int MaxY { get 
        {
            return _maxY;
        }
    }
    int _lastY = 1;
    private int _maxY = 0;
    private int LastY
    {
        get { return _lastY; }
        set { 
            if(value > MaxY)
            {
                _maxY = value;
            }
            _lastY = value;
        }
    }
    private int LastX => _generalPadding.PaddingX.Length 
        + _currentPadding.Length;
    private char PreviousInsert { get; set; }
    private enum Padding { Remove, Add };
    #region helpers
    private void AdjustPadding(Padding pad)
    {
        switch (pad)
        {
            case Padding.Remove:
                if(_currentPadding.Length > 0) _currentPadding.Remove(_currentPadding.Length - 1, 1);
                break;
            case Padding.Add:
                _currentPadding.Append(_chars.Space);
                break;
        }
    }
    private int GetLocalCaretPos(in StringBuilder buffer)
    {
        int Start;
        FindLocalStart(buffer,out Start);
        return Start + LastX;
    }
    private int FindLocalStart(in StringBuilder write, out int Start)
    {
        int findY = -1;
        int LineEnd = -1;
        int LineStart = -1;
        for (int i = 0; i < write.Length; i++)
        {
            if (write[i] == '\n') findY++;
            if (findY == LastY-1) LineEnd = i;
            if (findY == LastY-1 && LineStart < 0) LineStart = i+1;
        }
        Start = LineStart;
        Console.WriteLine("LastY:::" + LastY);
        Console.WriteLine("LineEnd:::" + LineEnd);
        Console.WriteLine("LineStart::" + Start);
        return LineEnd;
    }
    #endregion
    public StringBuilder InsertDown(int len, StringBuilder field)
    {
        var f = field;
        var Prev = GetLocalCaretPos(f)-1;
        bool right = false;
        if(PreviousInsert == _chars.Right)
        {
            right = true;
            AdjustPadding(Padding.Remove);
        }
        else if(PreviousInsert == _chars.Left)
        {

            // the test is skewed cus it's
            // adding even though it doesnt need to, on the last line
            // InsertLeft runs to the end, meaning the next character

            AdjustPadding(Padding.Add);
        }
        for(int i = 0; i < len; i++)
        {
            if(LastY+1 <= MaxY){
                LastY++;
                Console.WriteLine("Writing into {0}", LastY);
                int start = 0;
                int end = FindLocalStart(f, out start);
                var lineLen = end - start -1;
                if(lineLen < LastX)
                {
                    f.Insert(end, _chars.Space.ToString(), LastX - lineLen);
                }
                if (!(f[start+LastX] == '\r' 
                    || f[start+LastX] == _chars.NewLine))
                {
                    f[start + LastX] = _chars.Down;
                }
            }
            else 
            {
                LastY++;
                f.Append(
                    (_generalPadding.PaddingX + _currentPadding)
                    +_chars.Down 
                    +"\r\n");
            }
        }
        if (right)
        {
            AdjustPadding(Padding.Add);
        }
        this.PreviousInsert = _chars.Down;
        return f;
    }
    public StringBuilder InsertLeft(int len, StringBuilder field)
    {

        var write = field;
        int CaretPos  = GetLocalCaretPos(write);
        Console.WriteLine("CaretPos in Left is:::: {0}, ",
              CaretPos);
        if (CaretPos > 0 && field[CaretPos] == '\r') {
            CaretPos = CaretPos - 1;
            AdjustPadding(Padding.Remove);
        }
        var Traverse = new Func<StringBuilder>(
            () => {
            for(int i = CaretPos; i > CaretPos - len; i--)
            {
                AdjustPadding(Padding.Remove);
                switch (write[i])
                {
                    case 'R':
                        if (i > 0 && write[i -1] == ' ')
                        {
                           PreviousInsert = _chars.Left;
                           write[i] = _chars.Left;
                        }
                        else
                        {
                                write.Replace(write[i], ' ', i, 1);
                        }
                        break;
                    case 'L':
                            PreviousInsert = _chars.Left;
                            write[i] = _chars.Left;
                        break;
                    case ' ':
                            write[i] = _chars.Left;
                        break;
                    case '\n':
                        return write;
                    case '\r':
                            return write;
                    case 'D':

                            PreviousInsert = _chars.Left;
                            write[i] = _chars.Left;
                        break;
                    case 'U':

                            PreviousInsert = _chars.Left;
                            write[i] = _chars.Left;
                        break;
                }
            }
                return write;
        });
        return Traverse();
    }
    public StringBuilder InsertRight(int len, StringBuilder field)
    {
        int CaretPos = GetLocalCaretPos(field);
        var write = field;
        if (CaretPos - 1 > 0 && field[CaretPos - 1] == _chars.Down)
        {
            CaretPos = CaretPos - 1;
            AdjustPadding(Padding.Remove);
        }
        for (int i = 0; i < len; i++)
        {
            AdjustPadding(Padding.Add);
            if (CaretPos + i < field.Length && !(write[CaretPos + i] == '\r'
                || write[CaretPos + i] == _chars.NewLine))
            {
                write[CaretPos + i] = _chars.Right;
            }
            else
            {
                write.Insert(CaretPos, _chars.Right);
            }
        }
        PreviousInsert = _chars.Right;
        return write;
    }
    public StringBuilder InsertUp(int len, StringBuilder field)
    {

        var cPos = GetLocalCaretPos(field);
        bool downOverrider = false;
        if (PreviousInsert == _chars.Down)
        {
            AdjustPadding(Padding.Remove);
            for(int i = len; i > 0; i--)
            {
                cPos = GetLocalCaretPos(field);
                field.Replace(_chars.Down, _chars.Space, cPos, 1);
                LastY--;
            }
            return field;
        }
        if(PreviousInsert == _chars.Right)
        {
            AdjustPadding(Padding.Add);
        }
        for (int i = 0; i < len; i++)
        {

            if (LastY-1 < 1) return field;
            LastY--;
            var caretpos = GetLocalCaretPos(field);
            int LineStart = 0;
            int LineEnd = FindLocalStart(field, out LineStart);
            Console.WriteLine("Writing into {0}", LastY);
            int start = 0;
            int end = FindLocalStart(field, out start);
            var lineLen = end - start - 1;
            var diff = LastX - lineLen;
            if(lineLen < LastX && diff > 0)
            {
                field.Insert(end, _chars.Space.ToString(), diff);
            }
            if (caretpos < 0) return field;
            if (LineEnd < caretpos-2)
            {
                field.Insert(caretpos-1, _chars.Up);
                continue;
            }
            else if ((!(field[caretpos] == '\r') 
                || !(field[caretpos] == '\n')))
            {
                field[caretpos] = _chars.Up;
                continue;
            }
            else
            {
                field.Insert(caretpos, _chars.Up);
            }
        }
        PreviousInsert = _chars.Up;
        return field;
    }
    public object Clone()
    {
        return new PathWriter(this);
    }
}

