using System.ComponentModel.DataAnnotations;

namespace Corporate.Models;

public class BbqInput : IValidatableObject
{
    [Range(1, 10000, ErrorMessage = "Гостей должно быть минимум 1")]
    public int Guests { get; set; } = 10;

    [Range(1, 48, ErrorMessage = "Часы: от 1 до 48")]
    public int Hours { get; set; } = 4;

    public int Appetite { get; set; } = 1;

    public bool AlcoholEnabled { get; set; } = true;

    public int AlcoholMode { get; set; } = 0;

    [Range(0, 10000, ErrorMessage = "Пьющих не может быть отрицательно")]
    public int Drinkers { get; set; } = 0;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Drinkers > Guests)
            yield return new ValidationResult("Пьющих не может быть больше, чем гостей.", new[] { nameof(Drinkers) });

        // опционально: если алкоголь выключен/режим "без алкоголя", пьющих должно быть 0
        if ((!AlcoholEnabled || AlcoholMode == 4) && Drinkers > 0)
            yield return new ValidationResult("Если алкоголь не считается, количество пьющих должно быть 0.", new[] { nameof(Drinkers) });
    }
}

public class BbqResult
{
    public int Guests { get; set; }
    public int Drinkers { get; set; }

    public decimal MeatKg { get; set; }
    public int MeatPerPersonGr { get; set; }
    public decimal OnionKg { get; set; }
    public decimal VeggiesKg { get; set; }
    public int LavashPcs { get; set; }
    public decimal SauceL { get; set; }
    public decimal NonAlcoholL { get; set; }
    public decimal CoalKg { get; set; }

    public decimal BeerL { get; set; }
    public decimal WineL { get; set; }
    public decimal StrongL { get; set; }
}

public static class BbqCalculator
{
    public static BbqResult Calculate(BbqInput input)
    {
        var guests = input.Guests;

        // Коэффициент длительности: чем дольше, тем больше нужно
        // 1-3ч: меньше, 4-5ч: базово, 6-8ч: +15%, 9+ч: +30%
        decimal k = input.Hours switch
        {
            <= 3 => 0.90m,
            <= 5 => 1.00m,
            <= 8 => 1.15m,
            _ => 1.50m
        };

        // Мясо (г/чел): лёгкий 300, обычный 400, хороший 550
        var meatPerPerson = input.Appetite switch
        {
            0 => 300,
            2 => 550,
            _ => 400
        };

        // ЕДА (теперь умножаем на k)
        var meatKg = Math.Round((guests * meatPerPerson) / 1000m * k, 2);

        // Лук/маринад: ~20% от веса мяса
        var onionKg = Math.Round(meatKg * 0.20m, 2);

        // Овощи: 200 г/чел
        var veggiesKg = Math.Round((guests * 200m) / 1000m * k, 2);

        // Лаваш/хлеб: 1 шт на 2 человека (чуть увеличим на долгие посиделки)
        var lavashMultiplier = k >= 1.15m ? 1.10m : 1.00m;
        var lavash = (int)Math.Ceiling(guests / 2m * lavashMultiplier);

        // Соусы: 50 мл/чел
        var sauceL = Math.Round((guests * 50m) / 1000m * k, 2);

        // Вода/безалк: 0.6 л/чел
        var nonAlcoholL = Math.Round((0.6m * guests) * k, 2);

        // Уголь: 1 кг на 2 кг мяса (ориентир)
        var coalKg = Math.Round(meatKg * 0.5m, 2);

        // АЛКОГОЛЬ: 0 = никто не пьёт, считаем только если > 0
        int drinkers = 0;
        if (input.AlcoholEnabled && input.AlcoholMode != 4 && input.Drinkers > 0)
            drinkers = input.Drinkers;

        decimal beerL = 0, wineL = 0, strongL = 0;

        if (drinkers > 0)
        {
            // Нормы "на вечер" (базовые), потом умножаем на k
            switch (input.AlcoholMode)
            {
                case 0: // микс
                    beerL = 1.0m * drinkers;
                    wineL = 0.35m * drinkers;
                    strongL = 0.12m * drinkers;
                    break;

                case 1: // только пиво
                    beerL = 1.5m * drinkers;
                    break;

                case 2: // только вино
                    wineL = 0.6m * drinkers;
                    break;

                case 3: // только крепкое
                    strongL = 0.25m * drinkers;
                    break;
            }

            // Длительность влияет на алкоголь так же, как на еду
            beerL *= k;
            wineL *= k;
            strongL *= k;

            beerL = Math.Round(beerL, 2);
            wineL = Math.Round(wineL, 2);
            strongL = Math.Round(strongL, 2);
        }

        return new BbqResult
        {
            Guests = guests,
            Drinkers = drinkers,

            MeatKg = meatKg,
            MeatPerPersonGr = meatPerPerson,
            OnionKg = onionKg,
            VeggiesKg = veggiesKg,
            LavashPcs = lavash,
            SauceL = sauceL,
            NonAlcoholL = nonAlcoholL,
            CoalKg = coalKg,

            BeerL = beerL,
            WineL = wineL,
            StrongL = strongL
        };
    }
}

