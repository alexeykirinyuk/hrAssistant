package ru.tinkoff.models;

public class FileQuestion extends Question {
    public FileQuestion(String title, boolean required) {
        super(title, required);
    }

    @Override
    public QuestionType getQuestionType() {
        return QuestionType.File;
    }
}
