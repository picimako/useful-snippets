#if (${PACKAGE_NAME} && ${PACKAGE_NAME} != "")package ${PACKAGE_NAME};#end
#if ($NAME.endsWith("Steps"))
    import org.fluentlenium.adapter.cucumber.FluentCucumberTest;
    /**
     * Step definitions for the .
     */
    public class ${NAME} extends FluentCucumberTest {

    }
#elseif($NAME.endsWith("Util") || $NAME.endsWith("Utils"))
    /**
     * .
     */
    public final class ${NAME} {

    private ${NAME}() {
    //Utility class
    }
}
#else
    #parse("File Header.java")
    public class ${NAME} {

    }
#end
