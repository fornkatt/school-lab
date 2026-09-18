# 🎄 Laboration System25 – TDD med Advent of Code

I den här laborationen löser ni några små pussel från [Advent of Code](https://adventofcode.com)
med **testdriven utveckling** i C# och xUnit. Pusslen är små med flit.
Poängen är inte pusslet, utan sättet ni arbetar på:

1. **Röd** – skriv ett test som misslyckas.
2. **Grön** – skriv den *enklaste* kod som får testet att gå igenom.
3. **Refaktorera** – städa upp, kör testerna igen, fortfarande grönt.

Upprepa tills pusslet är löst. Skriv aldrig produktionskod utan ett fallande test först.

Varje pussel har exempel i sin text. De exemplen är era första tester. Lägg in dem i
`[InlineData]` innan ni skriver någon kod.

---

## 🎯 Lärandemål

Efter laborationen ska ni kunna:

- Skriva enhetstester med xUnit
- Skriva ren, testbar kod
- Skapa feltolerant kod genom att identifiera och hantera edge cases
- Använda testdriven utveckling som arbetssätt, inte som en efterhandskonstruktion

### 🗒️ Från kursplanen för G

- använda Test Driven Development
- skapa enhetstester
- skapa feltoleranta applikationer och system

### 📊 Bedömningskriterier

Ert arbete bedöms på:

1. **TDD-praxis** – bevis på röd-grön-refaktorera-cykeln. Committa efter varje grönt test
   och skriv testets namn i commit-meddelandet. Er historik ska läsas som en lista av tester.
2. **Testtäckning** – varje regel i pusslet har ett test. Varje exempel från pusseltexten
   är ett test.
3. **Testkvalitet** – små, välnamngivna tester. En sak per test. `[Theory]` för exempel,
   `[Fact]` för edge cases.
4. **Kodkvalitet** – korta metoder, tydliga namn, ingen logik i `Program.cs`.
5. **Korrekthet** – `dotnet test` är grönt och programmet skriver ut rätt svar.
6. **Edge cases** – ni har tänkt på tom indata, ett enda element, och fällorna vi pekar ut
   under varje pussel.

---

## 🏓 Arbetssätt: ping-pong-programmering

Arbeta i par. Ett tangentbord, två personer, och ni byter roll vid varje test:

1. **Spelare A** skriver ett fallande test och lämnar över tangentbordet.
2. **Spelare B** skriver den enklaste kod som får testet att gå igenom, refaktorerar vid
   behov, skriver sedan *nästa* fallande test och lämnar tillbaka tangentbordet.
3. **Spelare A** får det testet att gå igenom, skriver nästa, och så vidare.

Den som inte skriver är **navigatör**. Navigatörens jobb är att tänka framåt: vilket är
nästa minsta test? Vilken indata skulle knäcka koden vi just skrev? Tom sträng? Ett
element? Ett negativt tal?

Några regler som får det att fungera:

- **Prata innan ni skriver.** Kom överens om vad nästa test ska kontrollera innan någon
  skriver det.
- **Håll testerna små.** Om din partner behöver mer än ett par minuter för att få ett test
  grönt var testet för stort. Ta bort det och skriv ett mindre.
- **Hoppa inte över bytet.** Hela poängen är att båda skriver tester och båda skriver kod.
- **Committa vid varje grönt.** Skriv båda namnen i commiten.

---

## 🚀 Komma igång

Ni behöver ett konto på Advent of Code för att få er personliga indata. Logga in, öppna
pusslet och klicka på *get your puzzle input*. Spara den som `input.txt` i projektmappen.

### 🏁 Börja med det färdiga exemplet

Repot innehåller ett färdigt projektpar för det första pusslet, **Sonar Sweep**:

```
Y2021Day01/
  ├── Program.cs      läser input.txt och skriver ut svaren – ingen logik här
  ├── Sonar.cs        er logik – metoderna finns men kastar NotImplementedException
  └── input.txt       tom – klistra in er egen indata här
Y2021Day01.Tests/
  └── SonarTests.cs   det första testet är redan skrivet och är rött
```

Alla tre filerna är noggrant kommenterade. Läs dem innan ni skriver något. Kör sedan:

```bash
dotnet test
```

`Common.Tests` ska vara grönt och `Y2021Day01.Tests` ska vara **rött**. Det är meningen.
Öppna `Sonar.cs`, få testet grönt, skriv nästa test. Ni är igång.

### 🆕 Skapa projekt för nästa pussel

För varje pussel efter det första skapar ni ett konsolprojekt och ett testprojekt själva,
namngivna efter år och dag så att de inte krockar. Från roten av detta repo:

```bash
dotnet new console -n Y2015Day01
dotnet new xunit   -n Y2015Day01.Tests
dotnet sln add Y2015Day01 Y2015Day01.Tests
dotnet add Y2015Day01.Tests reference Y2015Day01
dotnet add Y2015Day01.Tests reference Common
dotnet add Y2015Day01 reference Common
dotnet test
```

Ta bort den genererade `UnitTest1.cs`. Lägg till detta i konsolprojektets `.csproj` så att
indatafilen kopieras till bin-mappen när det byggs (jämför med `Y2021Day01.csproj`). Det är
därför `Program.cs` läser filen via `AppContext.BaseDirectory`: då fungerar `dotnet run`
oavsett vilken mapp ni står i.

```xml
<ItemGroup>
  <None Update="input.txt" CopyToOutputDirectory="PreserveNewest" />
</ItemGroup>
```

### 🔀 Hur indatan kommer in i koden

Det här är exakt vad `Y2021Day01/Program.cs` gör, med kommentarer. Här är kortversionen.

Filen läses på **ett enda ställe**: i `Program.cs`. Den blir en sträng, strängen skickas
som argument till er klass, och klassen returnerar svaret. Klassen vet inte att det finns
någon fil.

```
input.txt  ──File.ReadAllText──▶  string  ──Input.Numbers──▶  int[]  ──▶  Sonar.CountIncreases(...)  ──▶  7
```

`Program.cs` är alltså bara limmet mellan filen och klassen:

```csharp
using Y2021Day01;
using AdventOfCode.Common;

var path = Path.Combine(AppContext.BaseDirectory, "input.txt");  // filen i bin-mappen
var text = File.ReadAllText(path);                                // hela filen som en sträng
var depths = Input.Numbers(text);                                 // strängen som int[]

Console.WriteLine($"Del 1: {Sonar.CountIncreases(depths)}");
Console.WriteLine($"Del 2: {Sonar.CountWindowIncreases(depths)}");
```

Och klassen tar emot färdig data, inte en fil:

```csharp
namespace Y2021Day01;

public static class Sonar
{
    public static int CountIncreases(int[] depths)
    {
        // ...
    }
}
```

**Testerna läser aldrig filen.** De anropar samma metod som `Program.cs` gör, men skickar
in exemplet från pusseltexten direkt:

```csharp
[Fact]
public void CountIncreases_PuzzleExample_Is7()
{
    // Arrange
    var depths = new[] { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263 };

    // Act
    var actual = Sonar.CountIncreases(depths);

    // Assert
    Assert.Equal(7, actual);
}
```

Det är därför logiken **inte** får ligga i `Program.cs`: då finns det ingen metod för
testet att anropa. Håll `Program.cs` till att läsa filen och skriva ut svaren, och lägg
allt annat i en separat klass.

För pussel där indatan är en enda rad (2015 Day 1, 2015 Day 10) behövs inte
`Input`-klassen. Skicka strängen direkt: `Floors.FinalFloor(text.Trim())`.

Kör testerna kontinuerligt medan ni arbetar:

```bash
dotnet watch test --project Y2015Day01.Tests
```

Advent of Code ber er att inte publicera er personliga indata. Lägg till `input.txt` i
`.gitignore` om ni pushar till ett publikt repo.

### 🧰 Den medföljande hjälpklassen: `Input`

Att parsa indatafiler är inte vad laborationen handlar om, så ni får en liten hjälpklass i
projektet `Common`. Den har två metoder:

| Metod | Vad den gör | Exempel |
|---|---|---|
| `Input.Lines(text)` | De icke-tomma, trimmade raderna | `"a\nb\n\nc"` → `["a", "b", "c"]` |
| `Input.Numbers(text)` | Alla heltal i texten, i ordning, oavsett vad som skiljer dem åt | `"3 4,5\n-6"` → `[3, 4, 5, -6]` |

Vissa pussel behöver den inte alls. Under varje pussel står vilken metod ni ska använda.

Titta på `Common.Tests/InputTests.cs` innan ni börjar. Den visar hur vi förväntar oss att
era tester ser ut.

### 🧪 Hur ett test ska se ut

Använd `[Theory]` med `[InlineData]` för exemplen. Använd kommentarerna Arrange / Act /
Assert som i Day05-demon:

```csharp
[Theory]
[InlineData("(())", 0)]
[InlineData("(((", 3)]
[InlineData("())", -1)]
public void FinalFloor_CountsUpAndDown(string input, int expected)
{
    // Arrange
    // Act
    var actual = Floors.FinalFloor(input);

    // Assert
    Assert.Equal(expected, actual);
}
```

---

# 🟢 Uppvärmning

Gör **alla** dessa. De är korta, och varje pussel har tillräckligt med exempel i sin text
för att ni aldrig ska behöva hitta på testdata. Vi listar exemplen här så att ni kan kopiera
dem.

## 🌊 1. Sonar Sweep — [2021 Day 1](https://adventofcode.com/2021/day/1)

**Det här pusslet finns färdigt uppsatt i `Y2021Day01`.** Börja här.

En lista med djupmätningar, en per rad.

**Hjälpklass:** `Input.Numbers(text)` ger er en `int[]`.

**Del 1** – Hur många mätningar är större än den föregående?

Exemplet `199 200 208 210 200 207 240 269 260 263` ger **7**.

**Del 2** – Samma sak, men jämför summor av glidande fönster om tre mätningar. Exemplet
ger **5**.

**Föreslagen ordning:** innan det stora exemplet, skriv tester för en tom array (0), ett
tal (0), två stigande tal (1) och två fallande tal (0). Sedan exemplet.

## 🎅 2. Not Quite Lisp — [2015 Day 1](https://adventofcode.com/2015/day/1)

Tomten följer `(` en våning upp och `)` en våning ner.

**Hjälpklass:** ingen. Hela indatan är en rad. Använd `text.Trim()`.

**Del 1** – Vilken våning hamnar han på?

| Indata | Förväntat |
|---|---|
| `(())` | 0 |
| `()()` | 0 |
| `(((` | 3 |
| `(()(()(` | 3 |
| `))(((((` | 3 |
| `())` | -1 |
| `))(` | -1 |
| `)))` | -3 |
| `)())())` | -3 |

**Del 2** – Positionen för det första tecknet som tar honom till våning -1. Positioner
börjar på 1, inte 0.

| Indata | Förväntat |
|---|---|
| `)` | 1 |
| `()())` | 5 |

**Föreslagen ordning:** testet för `(())`, sedan `(((`, sedan `())`. Skriv del 2 som en
separat metod. Ändra inte del 1 för att få del 2 att fungera.

## 🔢 3. Trebuchet?! — [2023 Day 1](https://adventofcode.com/2023/day/1)

Varje rad döljer ett tvåsiffrigt tal: den första siffran och den sista siffran på raden.
Summera alla.

**Hjälpklass:** `Input.Lines(text)`.

**Del 1**

| Rad | Förväntat |
|---|---|
| `1abc2` | 12 |
| `pqr3stu8vwx` | 38 |
| `a1b2c3d4e5f` | 15 |
| `treb7uchet` | 77 |

Summan av de fyra är **142**. Skriv ett test för en rad först, sedan ett test för summan.

**Del 2** – Utskrivna siffror räknas också: `one`, `two`, ... `nine`.

| Rad | Förväntat |
|---|---|
| `two1nine` | 29 |
| `eightwothree` | 83 |
| `abcone2threexyz` | 13 |
| `xtwone3four` | 24 |
| `4nineeightseven2` | 42 |
| `zoneight234` | 14 |
| `7pqrstsixteen` | 76 |

Summa **281**.

**Fälla:** orden kan överlappa. `eightwothree` börjar med `eight` *och* innehåller `two`.
`oneight` ska ge 18. Skriv testet för `eightwothree` **innan** ni implementerar del 2 och
se det misslyckas om ni ersätter ord med siffror från vänster till höger.

---

# 🟡 Lätt

Välj minst ett.

## 🗣️ Elves Look, Elves Say — [2015 Day 10](https://adventofcode.com/2015/day/10)

Läs en sträng av siffror högt: `111221` är "tre ettor, två tvåor, en etta", alltså blir
den `312211`. Gör det 40 gånger och rapportera längden.

**Hjälpklass:** ingen. Indatan är en kort sträng som ni kan klistra in direkt i
`Program.cs`.

Ni kan inte testa slutsvaret direkt. **Testa ett steg.** Pusslet ger er en kedja av fem
exempel, och varje länk är ett test:

| Indata | Förväntat |
|---|---|
| `1` | `11` |
| `11` | `21` |
| `21` | `1211` |
| `1211` | `111221` |
| `111221` | `312211` |

Skriv sedan en metod som gör steget *n* gånger och testa den med samma kedja: `1` fem
gånger är `312211`. Del 1 är längden efter 40 steg. Del 2 är 50.

**Föreslagen ordning:** `1` → `11` först (en följd av en siffra). Sedan `11` → `21` (ni
måste räkna). Sedan `21` → `1211` (flera följder). De två sista bör redan gå igenom.

**Fälla:** vid 50 iterationer är strängen några miljoner tecken lång. Om ert steg bygger
resultatet med `+=` på en sträng tar det väldigt lång tid. Byt till `StringBuilder`
*efter* att testerna är gröna, och kontrollera att de förblir gröna. Det är
refaktoreringssteget.

## 🚀 The Tyranny of the Rocket Equation — [2019 Day 1](https://adventofcode.com/2019/day/1)

Bränslet för en modul är dess massa delat med 3, avrundat nedåt, minus 2.

**Hjälpklass:** `Input.Numbers(text)`.

**Del 1** – Summan av bränslet för alla moduler.

| Massa | Bränsle |
|---|---|
| 12 | 2 |
| 14 | 2 |
| 1969 | 654 |
| 100756 | 33583 |

**Del 2** – Bränsle har också massa, så det behöver eget bränsle. Fortsätt tills bränslet
som behövs är noll eller negativt.

| Massa | Totalt bränsle |
|---|---|
| 14 | 2 |
| 1969 | 966 |
| 100756 | 50346 |

**Föreslagen ordning:** de fyra testerna för del 1, sedan summan. För del 2, skriv testet
för `14` först. Det är basfallet. Sedan `1969`.

---

# 🔴 Svårare

Gör dessa om ni vill ha en utmaning. De har var och en ett litet parsningssteg före
reglerna, och del 2 ändrar vad samma indata *betyder*.

## 🤿 Dive! — [2021 Day 2](https://adventofcode.com/2021/day/2)

Kommandon som `forward 5`, `down 3`, `up 2` flyttar en ubåt.

**Hjälpklass:** `Input.Lines(text)`, sedan delar ni upp varje rad själva.

**Parsning:** skriv en liten record och en `Parse`-metod, och testa den först:

```csharp
public record Command(string Direction, int Amount);

// Test: Command.Parse("forward 5") == new Command("forward", 5)
```

`line.Split(' ')` ger er de två delarna. Records jämförs på värde, så `Assert.Equal`
fungerar direkt på dem.

**Del 1** – `forward` adderar till horisontell position, `down` adderar till djupet, `up`
subtraherar. Svaret är horisontell position × djup.

Exemplet `forward 5, down 5, forward 8, up 3, down 8, forward 2` slutar på horisontell
position 15, djup 10, svar **150**.

**Del 2** – `down` och `up` ändrar nu *aim* i stället för djupet. `forward X` adderar X
till horisontell position och X × aim till djupet. Samma exempel slutar på horisontell
position 15, djup 60, svar **900**.

**Föreslagen ordning:** parsningstestet. Sedan ett ensamt `forward 5` (horisontell 5, djup
0). Sedan `down 5`. Sedan hela exemplet. Del 2 får en egen klass eller metod. Behåll
parsern.

## 🔐 Password Philosophy — [2020 Day 2](https://adventofcode.com/2020/day/2)

Varje rad är en lösenordspolicy och ett lösenord: `1-3 a: abcde`.

**Hjälpklass:** `Input.Lines(text)`. Radparsern **får ni färdig** nedan. Kopiera in den,
skriv testet för den, och börja med reglerna.

```csharp
public record PasswordEntry(int Min, int Max, char Letter, string Password)
{
    // "1-3 a: abcde"
    public static PasswordEntry Parse(string line)
    {
        var parts = line.Split(' ');       // ["1-3", "a:", "abcde"]
        var range = parts[0].Split('-');   // ["1", "3"]
        return new PasswordEntry(
            int.Parse(range[0]),
            int.Parse(range[1]),
            parts[1][0],
            parts[2]);
    }
}
```

**Del 1** – Lösenordet är giltigt om `Letter` förekommer mellan `Min` och `Max` gånger.

| Rad | Giltigt |
|---|---|
| `1-3 a: abcde` | ja |
| `1-3 b: cdefg` | nej |
| `2-9 c: ccccccccc` | ja |

**Del 2** – Talen är nu *positioner*, som börjar på 1. Exakt en av de två positionerna
måste innehålla `Letter`.

| Rad | Giltigt |
|---|---|
| `1-3 a: abcde` | ja |
| `1-3 b: cdefg` | nej |
| `2-9 c: ccccccccc` | nej |

**Fälla:** positioner börjar på 1, C#-index börjar på 0. Skriv testet för `2-9 c` i del 2
innan ni implementerar den. Lägg märke till att den byter från *ja* till *nej* mellan
delarna.

## ✂️ Rock Paper Scissors — [2022 Day 2](https://adventofcode.com/2022/day/2)

Varje rad är en omgång: motståndarens drag och ert. `A`/`B`/`C` är sten, påse, sax för
motståndaren. `X`/`Y`/`Z` är sten, påse, sax för er.

**Hjälpklass:** `Input.Lines(text)`, sedan är `line[0]` och `line[2]` de två bokstäverna.

**Poäng för en omgång** = er form (sten 1, påse 2, sax 3) + utfallet (förlust 0, oavgjort
3, vinst 6).

**Del 1**

| Omgång | Poäng |
|---|---|
| `A Y` | 8 |
| `B X` | 1 |
| `C Z` | 6 |

Totalt **15**.

**Del 2** – Den andra bokstaven betyder nu hur omgången ska *sluta*: `X` förlust, `Y`
oavgjort, `Z` vinst. Ni måste lista ut vilken form ni ska spela.

| Omgång | Poäng |
|---|---|
| `A Y` | 4 |
| `B X` | 1 |
| `C Z` | 7 |

Totalt **12**.

**Föreslagen ordning:** testa formpoängen för sig (`X` är 1). Testa utfallet för sig (sten
mot påse är vinst för påse). Sedan omgångens poäng. Sedan summan. Del 2 behöver en ny bit:
"vilken form slår / förlorar mot / blir oavgjort mot den här". Testa den för sig innan ni
kopplar in den.

**Tips:** en `enum Shape { Rock, Paper, Scissors }` och en metod som gör om en bokstav
till en `Shape` gör allt annat läsbart.

---

## ✅ När ni är klara

- Varje regel har ett eget test. Varje exempel från pusslet är ett test.
- Ni har minst ett edge case-test per pussel som *inte* kommer från pusseltexten.
- `dotnet test` är grönt.
- `Program.cs` skriver ut svaret för del 1 och del 2 av varje pussel ni gjort.
- Er commit-historik visar ping-pongen: en commit per grönt test, båda namnen på den.
- Ni skrev testet *före* koden. Varje gång.

Om ni blir klara tidigt: 2015 Day 5 från föreläsningen har en del 2 som vi inte gjorde
tillsammans.

---

## 📚 Resurser

- [xUnit-dokumentation](https://xunit.net/)
- [Test Driven Development](https://martinfowler.com/bliki/TestDrivenDevelopment.html) – Martin Fowler
- [Ping-pong-parprogrammering](https://martinfowler.com/articles/on-pair-programming.html#PingPong) – Martin Fowler

## ❓ Frågor?

Fråga era lärare. En del av inlärningen är att lista ut detaljerna genom era tester.

Lycka till! 🚀
