namespace Model.Constraints {
    public struct ConstraintResult {
        public readonly bool IsOk;
        public readonly string Error;

        private ConstraintResult(bool isOk, string error) {
            IsOk = isOk;
            Error = error;
        }

        public static ConstraintResult OkResult() {
            return new ConstraintResult(true, "");
        }

        public static ConstraintResult ErrorResult(string description) {
            return new ConstraintResult(false, description);
        }
    }
}