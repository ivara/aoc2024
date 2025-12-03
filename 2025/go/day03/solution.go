package main

import (
	"fmt"
	"slices"
	"strconv"
	"strings"
)

func part1(input string) int {
	sum := 0
	lines := strings.SplitSeq(input, "\n")
	for line := range lines {
		length := len(line)
		ogArr := make([]byte, length)

		// copy
		for i, r := range line {
			ogArr[i] = byte(r)
		}

		sortedOgArr := make([]byte, length-1)
		copy(sortedOgArr, ogArr[:len(ogArr)-1])
		// firstCopy := sortedOgArr[:length-1]
		slices.Sort(sortedOgArr)
		firstMaxValue := sortedOgArr[len(sortedOgArr)-1]

		// calculate 2nd
		maxValueIndex := slices.Index(ogArr, firstMaxValue)
		sortedSecondArr := make([]byte, len(ogArr[maxValueIndex+1:]))
		copy(sortedSecondArr, ogArr[maxValueIndex+1:])
		slices.Sort(sortedSecondArr)
		secondMaxValue := sortedSecondArr[len(sortedSecondArr)-1]

		fmt.Printf("Joltage for %v: %v%v\n", line, string(firstMaxValue), string(secondMaxValue))
		joltage := string(firstMaxValue) + string(secondMaxValue)
		jolt, _ := strconv.Atoi(joltage)
		sum += jolt
	}

	return sum
}

func part2(input string) int {
	sum := 0

	return sum
}
