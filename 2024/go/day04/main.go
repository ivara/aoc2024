package day04

import (
	"fmt"
	"log"
	"os"
	"strings"
)

var directions = [8][2]int{
	{1, 0},   // right
	{1, -1},  // right-down
	{0, -1},  // down
	{-1, -1}, // down-left
	{-1, 0},  // left
	{-1, 1},  // left-up
	{0, 1},   // up
	{1, 1},   // up-right
}

func main() {
	input := readFileContents("input.txt")
	result := part1(input)
	fmt.Println("Result: ", result)
}

func part1(input string) int {
	var sum = 0
	var lines = strings.Split(input, "\n")

	// Looooop over the grid
	for y, line := range lines {
		for x, rune := range line {
			if rune != 'X' {
				continue
			}

			// OK, we're on an X
			// check directions, can we find "XMAS" ?!
			for _, direction := range directions {
				if getWord(lines, y, x, direction) == "XMAS" {
					// do something...
					sum += 1
				}
			}
		}
	}
	return sum
}

func part2(input string) int {
	var sum = 0
	var lines = strings.Split(input, "\n")

	// skip looking outer border of grid
	// because we look for the A and the surrounding M and S
	for y := 1; y < len(lines)-1; y++ {
		line := lines[y]
		for x := 1; x < len(line)-1; x++ {
			if line[x] != 'A' {
				continue
			}
			backslashWord := string(lines[y-1][x-1]) + string(lines[y+1][x+1])
			slashWord := string(lines[y-1][x+1]) + string(lines[y+1][x-1])

			if isValidSlashword(backslashWord) && isValidSlashword(slashWord) {
				sum += 1
			}
		}
	}
	return sum
}

func isValidSlashword(surrounding string) bool {
	return surrounding == "MS" || surrounding == "SM"
}

func getWord(lines []string, row int, col int, direction [2]int) string {
	word := []byte{lines[row][col]}
	for {
		row += direction[1]
		col += direction[0]
		if row < 0 || row >= len(lines) || col < 0 || col >= len(lines[0]) {
			return ""
		}
		word = append(word, lines[row][col])
		if len(word) > 3 {
			break
		}
	}
	return string(word)
}

func readFileContents(filePath string) string {
	data, err := os.ReadFile(filePath)
	if err != nil {
		log.Fatal("Error reading file:", err)
	}

	return string(data)
}
