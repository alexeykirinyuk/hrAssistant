package ru.tinkoff.models;

public final class TextQuestion extends Question {
    public TextQuestion(String title, boolean required) {
        super(title, required);
    }

    @Override
    public QuestionType getQuestionType() {
        return QuestionType.Text;
    }
}
