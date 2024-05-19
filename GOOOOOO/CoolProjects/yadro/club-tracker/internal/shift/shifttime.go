package shifttime

import (
	"errors"
	"strconv"
	"strings"
)

type ShiftTime struct {
	Hour, Minute int
}

func NewTime(hour, minute int) ShiftTime {
	return ShiftTime{
		Hour:   hour,
		Minute: minute,
	}
}

func Parse(stringTime string) (ShiftTime, error) {
	parts := strings.Split(stringTime, ":")
	if len(parts) != 2 || len(parts[0]) != 2 || len(parts[1]) != 2 {
		return ShiftTime{}, errors.New("wrong time format")
	}
	hour, err := strconv.Atoi(parts[0])
	if err != nil {
		return ShiftTime{}, errors.New("can't parse hour " + parts[0])
	}
	minute, err := strconv.Atoi(parts[1])
	if err != nil {
		return ShiftTime{}, errors.New("can't parse minute " + parts[1])
	}
	return NewTime(hour, minute), nil
}

// Compare returns 1 if other shifttime is lower, 0 if equal and -1 if higher
func (t ShiftTime) Compare(other ShiftTime) int {
	hourRes := compareInts(t.Hour, other.Hour)
	if hourRes != 0 {
		return hourRes
	}
	return compareInts(t.Minute, other.Minute)
}

func compareInts(a, b int) int {
	if a > b {
		return 1
	}
	if a < b {
		return -1
	}
	return 0
}

func (t ShiftTime) Sub(start ShiftTime) ShiftTime {
	if t.Minute < start.Minute {
		return ShiftTime{
			Hour:   t.Hour - 1 - start.Hour,
			Minute: t.Minute + 60 - start.Minute,
		}
	}
	return ShiftTime{
		Hour:   t.Hour - start.Hour,
		Minute: t.Minute - start.Minute,
	}
}

func (t ShiftTime) Add(other ShiftTime) ShiftTime {
	minutes := t.Minute + other.Minute
	if minutes > 59 {
		t.Hour++
		minutes -= 60
	}
	return ShiftTime{
		Hour:   t.Hour + other.Hour,
		Minute: minutes,
	}
}

func (t ShiftTime) GetCeilHours() int {
	if t.Minute > 0 {
		return t.Hour + 1
	}
	return t.Hour
}
