package main

import (
	"strconv"
	"strings"
)

func part1(input string) int {
	max := 0
	chunks := strings.SplitSeq(input, "\n\n")
	for chunk := range chunks {
		sum := 0
		calories := strings.SplitSeq(chunk, "\n")
		for calorie := range calories {
			calInt, _ := strconv.Atoi(calorie)
			sum += calInt
		}
		if sum > max {
			max = sum
		}
	}
	return max
}
