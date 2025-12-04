package main

import (
	"strings"
)

func part1(input string) int {
	sum := 0
	lines := strings.Split(input, "\n")

	// Convert strings to byte slices
	byteLines := make([][]byte, len(lines))
	for i, line := range lines {
		byteLines[i] = []byte(line)
	}

	gridHeight := len(lines)
	gridWidth := len(lines[0])

	// walk through the grid
	for y := range gridHeight {
		for x := range gridWidth {
			if string(lines[y][x]) == "@" {
				// check surrounding 8 positions
				// if more than 3 has "@" this does not get added
				if isValidPaperRoll(byteLines, gridHeight, gridWidth, x, y) {
					sum += 1
				}
			}
		}
	}

	return sum
}

// 9773 too high
func part2(input string) int {
	sum := 0
	lines := strings.Split(input, "\n")
	// Convert strings to byte slices
	byteLines := make([][]byte, len(lines))
	for i, line := range lines {
		byteLines[i] = []byte(line)
	}

	gridHeight := len(lines)
	gridWidth := len(lines[0])

	// Loop until no more rolls found!
	for {
		currentIterationSum := 0
		// walk through the grid
		for y := range gridHeight {
			for x := range gridWidth {
				if string(byteLines[y][x]) == "@" {
					// check surrounding 8 positions
					// if more than 3 has "@" this does not get added
					if isValidPaperRoll(byteLines, gridHeight, gridWidth, x, y) {
						currentIterationSum += 1

						// IMPORTANT: remove the roll
						byteLines[y][x] = 'x'
					}
				}
			}
		}
		if currentIterationSum > 0 {
			sum += currentIterationSum
		} else {
			break
		}
	}
	return sum
}

// y = y-axis and x = x-axis
func isValidPaperRoll(grid [][]byte, gridHeight, gridWidth, x, y int) bool {
	surroundingPaperRolls := 0

	for y2 := y - 1; y2 <= y+1; y2++ {
		// out of bounds?
		if y2 < 0 || y2 >= gridHeight {
			continue
		}

		for x2 := x - 1; x2 <= x+1; x2++ {
			// out of bounds?
			if x2 < 0 || x2 >= gridWidth {
				continue
			}

			// Don't check self
			if y2 == y && x2 == x {
				continue
			}

			if string(grid[y2][x2]) == "@" {
				surroundingPaperRolls += 1
			}

			// exit early if condition already achieved
			if surroundingPaperRolls > 3 {
				return false
			}
		}
	}
	return true
}
