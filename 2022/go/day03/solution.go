package main

import (
	"strings"
)

func part1(input string) int {
	sum := 0

	for rucksack := range strings.SplitSeq(input, "\n") {
		half := len(rucksack)
		compartment1 := rucksack[:half]
		compartment2 := rucksack[half:]

		commonItemTypes := commonLettersString(compartment1, compartment2)
		sum += scoreItemTypes(commonItemTypes)
	}
	return sum
}

func scoreItemTypes(commonItemTypes string) int {
	sum := 0
	for _, r := range commonItemTypes {
		if r >= 'a' && r <= 'z' {
			sum += int(r) - int('a') + 1 // 1-26
		} else if r >= 'A' && r <= 'Z' {
			sum += int(r) - int('A') + 27 // 27-52
		}
	}
	return sum
}

func part2(input string) int {
	return 1
}

func commonLettersString(s1, s2 string) string {
	charMap := make(map[rune]bool)
	for _, char := range s1 {
		charMap[char] = true
	}

	common := make(map[rune]bool)
	for _, char := range s2 {
		if charMap[char] {
			common[char] = true
		}
	}

	result := ""
	for char := range common {
		result += string(char)
	}
	return result
}
