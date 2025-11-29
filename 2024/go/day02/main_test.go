package main

import "testing"

func TestPart1(t *testing.T) {
	input := `7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9`

	expected := 2
	got := part1(input)

	if got != expected {
		t.Errorf("Got = %v; want %v", got, expected)
	}
}

func TestPart2(t *testing.T) {
	input := `7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9`

	expected := 4
	got := part2(input)

	if got != expected {
		t.Errorf("Got = %v; want %v", got, expected)
	}
}
