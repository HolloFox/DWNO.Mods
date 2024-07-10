# DWNO.Mods
My mods for Digimon World Next Order

## Battle Stats Multiplier
This mod allows you to multiply the stats of your digimon via 8 configs.
The configs are as follows:
    - BattleRateAdd (Default: 0)
    - BattleRateMultiply (Default: 1)
    - battleRateGenAdd (Default: 0)
    - battleRateGenMultiply (Default: 0.5)
    - trainingRateAdd (Default: 0)
    - trainingRateMultiply (Default: 1)
    - trainingRateGenAdd (Default: 0)
    - trainingRateGenMultiply (Default: 0.5)

The Formula for the stats is as follows:
```CSharp
multiplier = (RateMultipliy + (GenMultiply * (Digimon Generation - 1)));
adder = (RateAdd + (GenAdd * (Digimon Generation - 1)));
stat = (baseStat * multiplier) + adder;
```
This greater customization allows for a more tailored experience for the player.
You can now have your digimon gain stats faster based on their generation. By default your partners will gain 50% more xp per generation after the first.
To configure: Change values and in the config file with your favorite text editor.

## Botamon Menu (Alpha Release)
Removes first 2 Botamon Dialog

## Build Settings Plugin
This mod allows time to build to be instant instead of having to wait an entire day.
You will still need to visit that part of town to trigger the build, but it will be instant.

## More Resolutions plugin (1440p and 4k support)
This mod brings in support for the following screen resolutions:
- 2304 x 1440
- 2560 x 1440
- 3840 x 2160
- 3840 x 2400