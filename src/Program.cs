using Labyrinth.Composition;
using Labyrinth.Composition.Interfaces;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
///****************************************************************
/// 
///     Proj Def: 
///     
///     Create a console app that renders a labirynth
///     Set a user character as some sign.
///     Create a Target to walk towards. (Treasure Item)
///     Make the labirynth expand and become slower.
/// 
///     We'll run it in powershell to make sure we can render Emotes.
///     
/// <summary>
/// ****************************************************************
/// do we do a model view binding?
///     lets try to avoid it.
/// </summary>
/// 
var builder = new HostApplicationBuilder();
builder.Services.AddScoped<ILine, Line>();
builder.Services.AddTransient<IParameterizedFactory<ICoordinates>, ParamFactory<ICoordinates>>(
    (x) => {
        return new ParamFactory<ICoordinates>((items) =>
        {
            return new WritingCoordinates((int)items[0], (int)items[0], (char)0);
        });
            
    });
//builder.Services.AddSingleton<IParameterizedFactory<IMemento<IInsertion>>>((x) => {
//    return new ParamFactory<IMemento<IInsertion>>(() => { })
//})
builder.Services.AddTransient<ILineFactoryFacade>(
    (ServiceP)=>{
        var a =
        ServiceP.GetRequiredService<ISimpleFactory<ICompareLineByStart<ILine>>>();
    return new LineFact(a);
});
builder.Services.AddTransient<ISimpleFactory<ICompareLineByStart<ILine>>>(
    (ServiceP) => {
    return new SimpleFactory<ICompareLineByStart<ILine>>(() =>
    {
        return new ComparebyStart();
    });
});
builder.Services.AddSingleton<ISimpleFactory<Characters>, SimpleFactory<Characters>>(x => {
    //so how do i cast this to a type? 
    return new SimpleFactory<Characters>(() =>
    {
        var chars = new Characters();
        builder.Configuration.GetSection("Characters").Bind(chars);
        return chars;
    });
});


builder.Services.AddScoped<ICoordinates, WritingCoordinates>();
var serviceHost = builder.Build();
var items = builder.Services.BuildServiceProvider();


var res = items.GetService<ISimpleFactory<Characters>>();


public class TestObject(ISimpleFactory<Characters> character)
{
    readonly ISimpleFactory<Characters> charFactory = character;

    public char ShowingChars()
    {
        var res = charFactory.Create();

        return res.Right;
    }

}
public interface ICommand
{
    public event EventHandler? CanExecuteChanged;
    public bool CanExecute(Func<int, StringBuilder[], StringBuilder> execute);
    public void Execute(int command);
}
public class Characters {
    public char Down    { get; init; }
    public char Up      { get; init; }
    public char Right   { get; init; }
    public char Left    { get; init; }
    public char Space   { get; init; }
    public char NewLine { get; init; }
}
public interface ISimpleFactory<T>
{
    public T Create();
}
public class SimpleFactory<T>(Func<T> implementation) : ISimpleFactory<T>
{
    private readonly Func<T> _implementation = implementation;
    public T Create()
    {
        return _implementation.Invoke();
    }
}
public class GeneralPadding
{
    public string PaddingY { get; set; }
    public string PaddingX { get; set; }
}
public interface IParameterizedFactory<T>
{
    public T Create(params object[] items);
}
public class ParamFactory<T> : IParameterizedFactory<T>
{
    readonly Func<object[], T> _implementation;
    public T Create(params object[] items)
    {
        return _implementation.Invoke(items);
    }
    
    public ParamFactory(Func<object[], T> implementation)
    {
        _implementation = implementation;
    }
}

//    public class PathDefiner
//    {
//        private event EventHandler<LabyrinthChangedEventArgs>?  LabyrinthChagned;
//    
//        private void OnLabChanged(object a, LabyrinthChangedEventArgs b)
//        {
//            this._path = string.Empty;
//        }
//    
//        private LabyrinthChangedEventArgs _args;
//        
//        //command pattern?
//        private int CaretPosition { get; set; }
//        private int startWidth = 10;
//        private int _width = 20;
//        private int _height = 20;
//        private string _path = null;
//        public string Path { 
//            get
//            {
//                if (_path is null || _path.Length < 60) 
//                {
//                    GeneratePath(this._width, this._height);
//                }
//                return _path;
//            }
//        }
//        public void ChangeLabDimensions(int width, int height)
//        {
//            _args = new LabyrinthChangedEventArgs()
//            {
//                Width = width,
//                Height = height
//            };
//            _width = width;
//            _height = height;
//    
//            LabyrinthChagned?.Invoke(this, _args);
//        }
//    
//    private void GeneratePath(int width, int height)
//    {
//        GlobalDirection previous = GlobalDirection.Down;
//        int a = 10;
//        StringBuilder path = new StringBuilder();
//        while(!(a == 0)) { 
//
//            var Direction = new Random().Next();
//            var len = new Random().Next((int)_height/2);
//            GlobalDirection currentDirection = GenerateDirectionHelper(Direction);
//            _path += WalkByDirection(currentDirection, len, path, previous).ToString();
//            a--;
//            Console.WriteLine(a);
//            previous = currentDirection;
//        }
//    }
//    private StringBuilder WalkByDirection(GlobalDirection dir, int len, in StringBuilder builder, string padding)
//    {
//        return dir switch
//        {
//            GlobalDirection.Right => GenerateRightDirection(len, builder, previous),
//            GlobalDirection.Left => GenerateLeftDirection(len, builder, previous),
//            _ => GenerateDown(len, builder, previous)
//        };
//    }

//    private GlobalDirection GenerateDirectionHelper(int number)
//    {
//        var result = number % 3;
//
//        return result switch
//        {
//            0 => GlobalDirection.Right,
//            1 => GlobalDirection.Left,
//            2 => GlobalDirection.Down
//        };
//    }
//    private enum GlobalDirection { Up, Down, Left, Right }
//}

public class LabyrinthChangedEventArgs : EventArgs
{
    public int Width { get; init; }
    public int Height { get; init; }
}

