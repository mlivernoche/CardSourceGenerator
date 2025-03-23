# CardSourceGenerator
This is an analyzer for generating types based on Yu-Gi-Oh card names. This allows card name errors (e.g., typos) to be detected by the compiler (i.e., using the wrong card name throws a compilation error). You can access these card names via `YGOCards.CardNameMap` or by going `YGOCards.[YourCardName]`.

## Related Repositories
1. [CardSourceGenerator](https://github.com/mlivernoche/CardSourceGenerator)
2. [YGOHandAnalysisFramework](https://github.com/mlivernoche/YGOHandAnalysisFramework)
3. [AdvancedDeckBuilder](https://github.com/mlivernoche/AdvancedDeckBuilder)

## How To Install
1. Add the analyzer to your project. It can be a project reference or a package.
2. Then, you need to provide the card data. Currently, the only supported format is the JSON data provided by YGOProDeck.com, [available via their API endpoint](https://ygoprodeck.com/api-guide/).
3. Add the JSON data to your project by downloading the JSON file from YGOProDeck, then adding it to your project (I usually add a folder called CardData and put it in there).
4. Next, you must tell the analyzer where the card data is. This can be done by editing the `.csproj` file with this property:

```
<ItemGroup>
	<AdditionalFiles Include="CardData\*.json" />
</ItemGroup>
```

5. I also include the JSON files in the `.gitignore` by including:

```
/{Project Folder}/CardData/*.json
```

6. If you want to include the data with your output so you can use it at runtime (e.g., loading attributes, types, attack points, etc.), you can also add this to the `.csproj`:

```
<ItemGroup>
  <AdditionalFiles Update="CardData\*.json">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </AdditionalFiles>
</ItemGroup>
```

## How To Use
Building your project will run the analyzer. If you're using Visual Studio, you can view these files by going `Solution Explorer` -> Your Project Here -> `Dependencies` -> `Analyzers` -> `CardSourceGenerator` -> `CardSourceGenerator.SourceGenerator`. You will have access to the `YGOCards` static class and the following types:

1. `YGOCards.StartingDeckLocation`
2. `YGOCards.IYGOCard`
3. `YGOCards.YGOCardName`

You will also have access to properties whose names are based on every Yu-Gi-Oh! card provided in all of the JSON card data files. You can view these in `YGOCards.Names.{FileNameHere}.g.cs`. These can be accessed by going `YGOCards.{CardNameHere}`.

Only the names are provided by the source generator, but you can also load more data (e.g., attack points, attributes, etc.) via helpers methods in the `YGOCards` static class. An example method is `YGOCards.LoadCardDataFromYgoPro(string path)` located in `YGOCards.CardData.g.cs`. Where these files are located depends on how you go about it, but I copy the JSON files with my build output (see step 6 in "How To Install") and I use this helper method:

```
public static class CardDataLoader
{
    public static IEnumerable<YGOCards.IYGOCard> LoadCardData()
    {
        var path = Path.Combine(Environment.CurrentDirectory, "CardData");
        var files = Directory.GetFiles(path, "*.json");
        return YGOCards
            .LoadAllCardDataFromYgoPro(files)
            .Values;
    }
}
```
