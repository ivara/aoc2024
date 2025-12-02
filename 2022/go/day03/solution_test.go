package main

import (
	"testing"
)

func TestPart1(t *testing.T) {
	input := `vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw`

	got := part1(input)
	want := 157
	// var b rune
	// b = 'A' // 65
	// fmt.Printf("%v", b)
	if got != want {
		t.Errorf("Got = %v; want %v", got, want)
	}
}

func TestPart2(t *testing.T) {
	input := `A Y
B X
C Z`

	got := part2(input)
	want := 12

	if got != want {
		t.Errorf("Got = %v; want %v", got, want)
	}
}
