package main

import (
	"fmt"
	"log"
	"os"
)

func main() {
	input := readFileContents("input.txt")
	result := part1(input)
	result2 := part2(input)

	fmt.Println("Result part1: ", result)
	fmt.Println("Result part2: ", result2)
}

func readFileContents(filePath string) string {
	data, err := os.ReadFile(filePath)
	if err != nil {
		log.Fatal("Error reading file:", err)
	}

	return string(data)
}
