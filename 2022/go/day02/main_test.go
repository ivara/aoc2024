package main

import "testing"

func TestPart1(t *testing.T) {
	input := `3   4
4   3
2   5
1   3
3   9
3   3`

	expected := 11
	got := part1(input)

	if got != expected {
		t.Errorf("Got = %v; want %v", got, expected)
	}
}
