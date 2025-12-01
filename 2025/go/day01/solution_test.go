package main

import (
	"testing"
)

func TestPart1(t *testing.T) {
	input := `L68
L30
R48
L5
R60
L55
L1
L99
R14
L82`

	got := part1(input)
	want := 3

	if got != want {
		t.Errorf("Got = %v; want %v", got, want)
	}
}

func TestPart2(t *testing.T) {
	input := `1000
2000
3000

4000

5000
6000

7000
8000
9000

10000
`

	got := part2(input)
	want := 45000

	if got != want {
		t.Errorf("Got = %v; want %v", got, want)
	}
}
