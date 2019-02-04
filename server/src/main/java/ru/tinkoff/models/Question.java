package ru.tinkoff.models;

import ru.tinkoff.helpers.Guard;

public abstract class Question {
    private final String title;
    private final boolean required;

    Question(String title, boolean required) {
        Guard.IsNullOrEmpty(title, "title");

        this.title = title;
        this.required = required;
    }

    public abstract QuestionType getQuestionType();

    public String getTitle() {
        return title;
    }

    public boolean isRequired() {
        return required;
    }
}
