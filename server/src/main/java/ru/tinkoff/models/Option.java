package ru.tinkoff.models;

import ru.tinkoff.helpers.Guard;

public final class Option {
    private final String title;

    public Option(String title) {
        Guard.IsNullOrEmpty(title, "title");

        this.title = title;
    }

    public String getTitle() {
        return title;
    }
}
