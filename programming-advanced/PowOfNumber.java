    class PowOfNumber {
        public static void main(String[] args) {
            int result  = Power(2);
            System.out.println(result);
        }

        public static int Power(int number){
            int result = 1;
            for(int i = 0; i < 2; i++){
                result *= number;
            }
            return result;
        }
    }