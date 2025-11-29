package day04

import "testing"

func TestGetWordTrue(t *testing.T) {
	// direction := int{1,0}
	lines := []string{"XMAS"}
	expected := "XMAS"
	got := getWord(lines, 0, 0, [2]int{1, 0})

	if got != expected {
		t.Errorf("Got = %v; want %v", got, expected)
	}
}

func TestGetWordFalse(t *testing.T) {
	// direction := int{1,0}
	lines := []string{"ivar"}
	wrong := "XMAS"
	got := getWord(lines, 0, 0, [2]int{1, 0})

	if got == wrong {
		t.Errorf("Got = %v; wrong value is %v", got, wrong)
	}
}
func TestPart1(t *testing.T) {
	input := `MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX`

	expected := 18
	got := part1(input)
	if got != expected {
		t.Errorf("Got = %v; want %v", got, expected)
	}
}

func TestMainPart1(t *testing.T) {
	input := readFileContents("input.txt")

	expected := 2514
	got := part1(input)

	if got != expected {
		t.Errorf("Got = %v; want %v", got, expected)
	}
}
func TestMainPart2(t *testing.T) {
	input := readFileContents("input.txt")

	expected := 1888
	got := part2(input)

	if got != expected {
		t.Errorf("Got = %v; want %v", got, expected)
	}
}

func TestPart2(t *testing.T) {
	input := `.M.S......
..A..MSMS.
.M.S.MAA..
..A.ASMSM.
.M.S.M....
..........
S.S.S.S.S.
.A.A.A.A..
M.M.M.M.M.
..........`

	expected := 9
	got := part2(input)

	if got != expected {
		t.Errorf("Got = %v; want %v", got, expected)
	}
}
