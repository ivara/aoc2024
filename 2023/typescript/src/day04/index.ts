import run from "aocrunner";

const parseInput = (rawInput: string) => rawInput.split("\n");

const part1 = (rawInput: string) => {
  const lines = parseInput(rawInput);
  let sum = 0;
  for (let i = 0; i < lines.length; i++) {
    // figure out the winner numbers
    // and "my numbers"
    let [theirs, mine] = lines[i]
      .split(":")[1]
      .split("|")
      // .map((x) => x.trim().split(" ").filter(parseInt));
      .map((x) => x.trim().split(" ").filter(Boolean).map(Number));

    // console.log("theirs", theirs);
    // now figure out how many of my numbers are in their numbers
    let mySet = new Set(mine);
    var wins = 0;
    for (let t = 0; t < theirs.length; t++) {
      // console.log("checking", theirs[t]);
      if (mySet.has(theirs[t])) {
        wins++;
      }
    }

    if (wins > 0) {
      var winsum = 1 << (wins - 1);

      sum += winsum;

      // console.log(
      //   "card " + (i + 1),
      //   ": win sum: ",
      //   winsum,
      //   " (total: ",
      //   sum,
      //   ")",
      // );
    } else {
      //console.log("card " + (i + 1), ": no wins");
    }
  }

  return sum;
};

const part2 = (rawInput: string) => {
  console.log("part2");
  const lines = parseInput(rawInput);
  let numberOfScratchCards = 0;

  // parse the original scratch cards
  for (let i = 0; i < lines.length; i++) {
    let foundCopies = 0;
    numberOfScratchCards += 1;
    foundCopies += getCopiesCountRecursive(i, lines, 0);
    console.log("found copies", foundCopies);
    numberOfScratchCards += foundCopies;
  }

  return numberOfScratchCards;
};

// recursive
function getCopiesCountRecursive(currentLine: number, lines: string[], depth: number): number {
  console.log("getCopiesCountRecursive with depth", depth, "currentLine", currentLine, "lines.length", lines.length);
  let sum = 1;

  let [theirs, mine] = lines[currentLine]
    .split(":")[1]
    .split("|")
    // .map((x) => x.trim().split(" ").filter(parseInt));
    .map((x) => x.split(" ").filter(Boolean).map(Number));

  let mySet = new Set(mine);
  var matches = 0;
  for (let t = 0; t < theirs.length; t++) {
    // console.log("checking", theirs[t]);
    if (mySet.has(theirs[t])) {
      matches++;
    }
  }
  //console.log("currentLine ", currentLine, ".. found matches: ", matches);
  // if current card has no matches, then no copies!
  if (matches === 0) return 0;

  // add copies to the sum...
  for (let x = 0; x < matches; x++) {
    const nextLine = currentLine + (x + 1);
    if (nextLine > lines.length) break;
    sum += getCopiesCountRecursive(nextLine, lines, ++depth);
  }

  return sum;
}

run({
  part1: {
    tests: [
      // {
      //   input: `
      //   Card 207: 51 22 90  8 72 78 61 97 25 24 | 93 59 91 87  2 28 35 16 76 34 49 63 48 98 83 37 85 13 67  4 18 30 43 60 51
      //   `,
      //   expected: 1,
      // },
      {
        input: `
        Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
        Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
        Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
        Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
        Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
        Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
        `,
        expected: 13,
      },
    ],
    solution: part1,
  },
  part2: {
    tests: [
      {
        input: `
        Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
        Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
        Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
        Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
        Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
        Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
        `,
        expected: 30,
      },
    ],
    solution: part2,
  },
  trimTestInputs: true,
  onlyTests: true,
});
