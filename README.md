# Naticon #

## Introduction ##

A natural language parser Dates and Times for .Net written in C#.

## Usage ##

```
DateTime.Now;
  //=> Sun Aug 27 23:18:25 PDT 2006

var parser = new Parser();
parser.Parse("tomorrow");
  // => Mon Aug 28 12:00:00 PDT 2006

// Setting context
parser = new Parser(new Options { Context = Pointer.Type.Past });
parser.Parse("monday");
  // => Mon Aug 21 12:00:00 PDT 2006

parser = new Parser(new Options { Clock = () => new DateTime(2000, 1, 1)});
parser.Parse("may 27th");
  // => Sat May 27 12:00:00 PDT 2000
```

## Credits ##

Forked from the [nChronic](https://github.com/robertwilczynski/nChronic) project which has since been abandoned.

## License ##

Unless specified otherwise all is licensed under the MIT license. See LICENSE for details.