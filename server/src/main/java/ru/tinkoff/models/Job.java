package ru.tinkoff.models;

import ru.tinkoff.helpers.Guard;

public final class Job {
    private final String code;
    private final String title;

    public Job(String code, String title) {
        Guard.IsNullOrEmpty(code, "code");
        Guard.IsNullOrEmpty(title, "title");

        this.code = code;
        this.title = title;
    }
    public String getCode() {
        return code;
    }

    public String getTitle() {
        return title;
    }
}
