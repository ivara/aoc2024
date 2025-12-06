import run from "aocrunner";

const parseInput = (rawInput: string) => rawInput;

type Game = {
  id: number;
  maxRed: number;
  maxGreen: number;
  maxBlue: number;
}

const part1 = (rawInput: string) => {
  const input = parseInput(rawInput);

  let games: Game[] = [];

  var lines = input.split('\n');
  lines.forEach(line => {
    let data = line.split(':');
    var gameId = data[0].substring(4);
    var maxRed = 0;
    var maxGreen = 0;
    var maxBlue = 0;

    var sets = data[1].split(';');
    sets.forEach(set => {
      var colors = set.split(',');
      colors.forEach(color => {
        color = color.trim();
        var colorData = color.split(' ');
        var amount = colorData[0];
        var colorName = colorData[1];
        switch(colorName) {
          case 'red':
            maxRed = Math.max(maxRed, parseInt(amount));
            break;
          case 'green':
            maxGreen = Math.max(maxGreen, parseInt(amount));
            break;
          case 'blue':
            maxBlue = Math.max(maxBlue, parseInt(amount));
            break;
        }
      });
    });
    games.push({
      id: parseInt(gameId),
      maxRed: maxRed,
      maxGreen: maxGreen,
      maxBlue: maxBlue,
    });

  });

  // 12 red cubes, 13 green cubes, and 14 blue cubes
  var possibleGames = games.filter(game => game.maxRed <= 12 && game.maxGreen <= 13 && game.maxBlue <= 14);
  const sumGameIds = possibleGames.reduce((sum, game) => sum + game.id, 0);

  return sumGameIds
};

const part2 = (rawInput: string) => {
  const input = parseInput(rawInput);

  let games: Game[] = [];

  var lines = input.split('\n');
  lines.forEach(line => {
    let data = line.split(':');
    var gameId = data[0].substring(4);
    var maxRed = 0;
    var maxGreen = 0;
    var maxBlue = 0;

    var sets = data[1].split(';');
    sets.forEach(set => {
      var colors = set.split(',');
      colors.forEach(color => {
        color = color.trim();
        var colorData = color.split(' ');
        var amount = colorData[0];
        var colorName = colorData[1];
        switch(colorName) {
          case 'red':
            maxRed = Math.max(maxRed, parseInt(amount));
            break;
          case 'green':
            maxGreen = Math.max(maxGreen, parseInt(amount));
            break;
          case 'blue':
            maxBlue = Math.max(maxBlue, parseInt(amount));
            break;
        }
      });
    });
    games.push({
      id: parseInt(gameId),
      maxRed: maxRed,
      maxGreen: maxGreen,
      maxBlue: maxBlue,
    });

  });

  // 12 red cubes, 13 green cubes, and 14 blue cubes
  //var possibleGames = games.filter(game => game.maxRed <= 12 && game.maxGreen <= 13 && game.maxBlue <= 14);
  const sumGameIds = games.reduce((sum, game) => sum + game.maxBlue*game.maxGreen*game.maxRed, 0);

  return sumGameIds
};

run({
  part1: {
    tests: [
      // {
      //   input: ``,
      //   expected: "",
      // },
    ],
    solution: part1,
  },
  part2: {
    tests: [
      // {
      //   input: ``,
      //   expected: "",
      // },
    ],
    solution: part2,
  },
  trimTestInputs: true,
  onlyTests: false,
});
