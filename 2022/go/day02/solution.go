package main

import (
	"strings"
)

func part1(input string) int {
	sum := 0
	scoreMap := map[string]int{
		"A X": 1 + 3, // Rock vs Rock = Draw (1 for Rock + 3 for draw)
		"A Y": 2 + 6, // Rock vs Paper = Win (2 for Paper + 6 for win)
		"A Z": 3 + 0, // Rock vs Scissors = Loss (3 for Scissors + 0 for loss)
		"B X": 1 + 0, // Paper vs Rock = Loss
		"B Y": 2 + 3, // Paper vs Paper = Draw
		"B Z": 3 + 6, // Paper vs Scissors = Win
		"C X": 1 + 6, // Scissors vs Rock = Win
		"C Y": 2 + 0, // Scissors vs Paper = Loss
		"C Z": 3 + 3, // Scissors vs Scissors = Draw
	}
	for line := range strings.SplitSeq(input, "\n") {
		sum += scoreMap[line]
	}
	return sum
}

func part2(input string) int {
	sum := 0
	// X = lose = 0
	// Y = draw = 3
	// Z = win = 6
	// A = rock = 1
	// B = Paper = 2
	// C = Scissor = 3
	scoreMap := map[string]int{
		"A X": 3 + 0, // Rock and lose (Scissor) = 3 + 0
		"A Y": 1 + 3, // Rock and Draw
		"A Z": 2 + 6, // Rock and win (Paper) = 2 + 6
		"B X": 1 + 0, // Paper and lose (rock) = 1 + 0
		"B Y": 2 + 3, // Paper and draw (Paper) = 2 + 3
		"B Z": 3 + 6, // Paper and win (Scissor) = 3 + 6
		"C X": 2 + 0, // Scissors and lose (paper) = 2
		"C Y": 3 + 3, // Scissors and draw (Scissors) = 3 + 3
		"C Z": 1 + 6, // Scissors and win (rock) = 1 + 6 = 7
	}
	for line := range strings.SplitSeq(input, "\n") {
		sum += scoreMap[line]
	}
	return sum
}
