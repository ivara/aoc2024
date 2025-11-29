package main

import (
	"fmt"
	"log"
	"os"
	"slices"
	"strconv"
	"strings"
)

func main() {
	input := readFileContents("test.txt")
	result := part1(input)

	fmt.Println("Result: ", result)
}

func part1(input string) int {
	list1, list2 := parseInput(input)

	slices.Sort(list1)
	slices.Sort(list2)
	var sum int

	for i := range list1 {
		sum += absInt(list1[i] - list2[i])
	}
	return sum
}

func part2(input string) int {
	list1, list2 := parseInput(input)

	countsList2 := map[int]int{}
	for _, v := range list2 {
		countsList2[v]++
	}

	ans := 0
	for _, v := range list1 {
		ans += v * countsList2[v]
	}
	return ans
}

func readFileContents(filePath string) string {
	data, err := os.ReadFile(filePath)
	if err != nil {
		log.Fatal("Error reading file:", err)
	}

	return string(data)
}

func parseInput(input string) (list1, list2 []int) {
	for line := range strings.SplitSeq(input, "\n") {
		if strings.TrimSpace(line) == "" {
			continue
		}
		nums := strings.Split(line, "   ")
		val1, _ := strconv.Atoi(nums[0])
		val2, _ := strconv.Atoi(nums[1])
		list1 = append(list1, val1)
		list2 = append(list2, val2)
	}

	return list1, list2
}

func absInt(x int) int {
	if x < 0 {
		return -x
	}
	return x
}
