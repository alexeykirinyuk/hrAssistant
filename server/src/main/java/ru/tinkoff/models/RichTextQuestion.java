package ru.tinkoff.models;

public final class RichTextQuestion extends Question {
    public RichTextQuestion(String title, boolean required) {
        super(title, required);
    }

    @Override
    public QuestionType getQuestionType() {
        return QuestionType.RichText;
    }
}
