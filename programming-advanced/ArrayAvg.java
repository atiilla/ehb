public class ArrayAvg{
    public static void main(String[] args) {
        int[] studentenScores = {12, 8, 13, 18, 15, 6, 12};
        int grootte = studentenScores.length;
        System.out.println("Avarege score: " + avgScore(studentenScores, grootte));

        
    }

    public static int avgScore (int[] scores, int grootte){
        int sum=0;
        for (int i=0; i<grootte; i++){
            sum += scores[i];
        }
        return sum/scores.length;
    }
}