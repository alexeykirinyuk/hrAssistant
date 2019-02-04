package ru.tinkoff.helpers;

public final class Guard {
    public static void IsNotNull(Object parameter, String paramName) {
        if (parameter == null) {
            throw new IllegalArgumentException(paramName + " can't be null.");
        }
    }

    public static void IsNullOrEmpty(String parameter, String paramName) {
        Guard.IsNotNull(parameter, paramName);

        if (parameter.isEmpty()) {
            throw new IllegalArgumentException(paramName + " can't be empty.");
        }
    }
}
