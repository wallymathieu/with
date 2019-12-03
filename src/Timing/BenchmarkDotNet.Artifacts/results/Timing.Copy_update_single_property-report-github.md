``` ini

BenchmarkDotNet=v0.10.10, OS=Windows 10.0.19033
Processor=Intel Core i7-8650U CPU 1.90GHz, ProcessorCount=8
.NET Core SDK=3.0.100
  [Host]     : .NET Core 2.1.13 (Framework 4.6.28008.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.13 (Framework 4.6.28008.01), 64bit RyuJIT


```
|                                     Method |     Mean |    Error |   StdDev |
|------------------------------------------- |---------:|---------:|---------:|
|      Using_static_prepered_copy_expression | 341.4 ns | 6.383 ns | 5.971 ns |
| Hand_written_method_returning_new_instance | 358.2 ns | 4.840 ns | 4.041 ns |
