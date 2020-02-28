package no.test.report.details;

class MyRuntimeException extends RuntimeException {

    public MyRuntimeException(String string, Exception e) {
        super(string, e);
    }

    /**
     * blah
     */
    private static final long serialVersionUID = 7073808601445623820L;
}