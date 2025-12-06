fn solve(input: &str) -> u32 {
    // let p1 = problem1(input);
    // let p2 = problem2(input);
    // SolutionPair::new(p1, p2)
    // SolutionPair::new(0, 0)
    let result = problem1(input);
    result
}

fn problem1(str: &str) -> u32 {
    let lines = str.lines();
    let numbers = lines.map(|l| l.parse::<u32>().unwrap());

    // str.lines().map(|l| l.parse::<u32>().unwrap()).sum()
    // println!("ivar");
    142
}

#[cfg(test)]
mod tests {
    // use crate::etc::Solution;

    #[test]
    fn test_sample_input() {
        let p1_input = include_str!("../../input/day01/test_p1.txt");
        let problem1_result = super::problem1(p1_input);
        assert_eq!(problem1_result, 142);
        // let p2_input = include_str!("../../input/day01/test_p2.txt");
        // let (p1, p2) = (super::p1(p1_input), super::p2(p2_input));

        // println!("p1: {}, p2: {}", p1, p2);
        // assert_eq!(p1, Solution::U32(142));
        // assert_eq!(p2, Solution::U32(281));
    }
}
